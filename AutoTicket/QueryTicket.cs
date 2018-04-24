using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace AutoTicket
{
    public partial class QueryTicket : Form
    {
        public Dictionary<string, string> cacheDic = new Dictionary<string, string>();

        public QueryTicket()
        {
            InitializeComponent();
            InitBasicData();
            try
            {
                LoadCustomData();
            } catch { }
        }

        private void LoadCustomData()
        {
            cmbstartStation.Text = Util.ReadConfig("config/startPlaceName");
            cmbstartStation.SelectedValue = Util.ReadConfig("config/startPlaceCode");
            cmbendStation.Text = Util.ReadConfig("config/endPlaceName");
            cmbendStation.SelectedValue = Util.ReadConfig("config/endPlaceCode");
            DateTime n = DateTime.Now;
            DateTime.TryParse(Util.ReadConfig("config/trainDate"), out n);
            dtpTrainDate.Value = n;
        }

        /// <summary>
        /// 加载站点间数据，这里可能已经过时，手动输入的时候已经不是这几个站点了
        /// </summary>
        private void InitBasicData()
        {
            var url = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js?station_version=1.9051";
            var loginRes = HttpWebRequestExtension.GetWebContent(url, HttpWebRequestExtension._12306Cookies);
            SetStation(loginRes);
            SetPassengers();
        }

        private void SetPassengers()
        {

            var getPassenger = TicketBiz.GetTokenThenGetPassenger();
            AutoTicket.JSON.Passenger.RootObject passengerDTO = JsonConvert.DeserializeObject<AutoTicket.JSON.Passenger.RootObject>(getPassenger);

            ((System.Windows.Forms.ListBox)this.checkedListBox1).DisplayMember = "Key";
            ((System.Windows.Forms.ListBox)this.checkedListBox1).ValueMember = "Value";

            if (passengerDTO == null || passengerDTO.data == null || passengerDTO.data.normal_passengers == null) return;
            foreach (var item in passengerDTO.data.normal_passengers)
            {
                this.checkedListBox1.Items.Add(new
                {
                    Key = item.passenger_name,
                    Value = "0,1,"
                        + item.passenger_name + ",1," + item.passenger_id_no + "," + item.mobile_no + ",N"
                });
            }

            this.checkedListBox1.ClientSize = new Size(TextRenderer.MeasureText(checkedListBox1.Items[0].ToString(), checkedListBox1.Font).Width + 20,
                checkedListBox1.GetItemRectangle(0).Height * checkedListBox1.Items.Count);
            this.richTextBox1.Text += Environment.NewLine + "联系人已取出:" + getPassenger;
        }

        /// <summary>
        /// 拆分12306 站点数据
        /// </summary>
        /// <param name="station"></param>
        private void SetStation(string station)
        {
            var s = station.Split('=')[1].Replace("'", "").Replace(";", "").Split('|');
            List<ItemObject> allPlace = new List<ItemObject>();
            List<ItemObject> endPlace = new List<ItemObject>();
            for (int i = 0; i < s.Length; i++)
            {
                if (i % 5 == 0)
                {
                    if ((i + 1) < s.Length && (i + 4) < s.Length)
                    {
                        string statename = s[i + 1];
                        string code = s[i + 2];
                        cacheDic.Add(statename, code);
                        allPlace.Add(new ItemObject() { Name= statename, Value= code });
                        endPlace.Add(new ItemObject() { Name = statename, Value = code });
                    }
                }
            }
            
            cmbstartStation.DataSource = allPlace;
            cmbstartStation.DisplayMember = "Name";
            cmbstartStation.ValueMember = "Value";

            cmbendStation.DataSource = endPlace;
            cmbendStation.DisplayMember = "Name";
            cmbendStation.ValueMember = "Value";
        }

        /// <summary>
        /// 查询余票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //this.lblCDNSite.Text = CDNReset.GetCDN();
            this.richTextBox1.Text = "";

            //var logUrl = string.Format(TrainUrlConstant.LogLeftTicketLog, dtpTrainDate.Value.ToString("yyyy-MM-dd"),
            //    cmbstartStation.SelectedValue, cmbendStation.SelectedValue);

            var url = string.Format(TrainUrlConstant.TrainleftTicketInfo, dtpTrainDate.Value.ToString("yyyy-MM-dd"),
                cmbstartStation.SelectedValue, cmbendStation.SelectedValue);

            // seems no use any more
            //var trainLeftLog = HttpWebRequestExtension.GetWebContent(logUrl, HttpWebRequestExtension._12306Cookies);
            var trainleftTicketInfoRes = HttpWebRequestExtension.GetWebContent(url, HttpWebRequestExtension._12306Cookies);

            // 保存用户常用操作
            SaveCustomerUseful();

            RootObject obj = JsonConvert.DeserializeObject<RootObject>(trainleftTicketInfoRes);
            DataTable dt = new DataTable();
            QueryLeftNewDTO sample = new QueryLeftNewDTO();
            Util.ClassToDataRow<QueryLeftNewDTO>(dt, sample);
            dt.Columns.Add("secretStr");
            dt.Columns.Add("buttonTextInfo");

            int i = 0;
            foreach (var item in obj.data)
            {
                DataRow row = dt.NewRow();
                Util.ClassToField<QueryLeftNewDTO>(dt, row, item.queryLeftNewDTO);
                row["secretStr"] = item.secretStr;
                row["buttonTextInfo"] = item.buttonTextInfo;
                dt.Rows.Add(row);
            }

            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = dt;
            this.dataGridView1.Refresh();
            
            this.richTextBox1.Text = trainleftTicketInfoRes;

            SaveJCCookie();
        }

        private void SaveCustomerUseful()
        {
            Util.WriteConfig("config/startPlaceName", cmbstartStation.Text);
            Util.WriteConfig("config/startPlaceCode", cmbstartStation.SelectedValue.ToString());
            Util.WriteConfig("config/endPlaceName", cmbendStation.Text);
            Util.WriteConfig("config/endPlaceCode", cmbendStation.SelectedValue.ToString());
            Util.WriteConfig("config/trainDate", dtpTrainDate.Value.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        /// 在查询站的时候，需要存一下查询的记录到 cookie ，这里12306 会去检测
        /// </summary>
        private void SaveJCCookie()
        {
            HttpWebRequestExtension.cookieList.Remove("_jc_save_fromStation");
            HttpWebRequestExtension.cookieList.Add(
                "_jc_save_fromStation",
                new Cookie(
                        "_jc_save_fromStation",
                        Util.Escape(cmbstartStation.Text + "," + cmbstartStation.SelectedValue),
                        "/",
                        "kyfw.12306.cn")
                );

            HttpWebRequestExtension.cookieList.Remove("_jc_save_toStation");
            HttpWebRequestExtension.cookieList.Add("_jc_save_toStation",
                new Cookie(
                            "_jc_save_toStation",
                            Util.Escape(cmbendStation.Text + "," + cmbendStation.SelectedValue),
                            "/",
                            "kyfw.12306.cn")
                );

            HttpWebRequestExtension.cookieList.Remove("_jc_save_fromDate");
            HttpWebRequestExtension.cookieList.Add("_jc_save_fromDate",
                new Cookie(
                            "_jc_save_fromDate",
                            dtpTrainDate.Value.ToString("yyyy-MM-dd"),
                            "/",
                            "kyfw.12306.cn")
            );

            HttpWebRequestExtension.cookieList.Remove("_jc_save_toDate");
            HttpWebRequestExtension.cookieList.Add("_jc_save_toDate",
                new Cookie(
                            "_jc_save_toDate",
                            dtpTrainDate.Value.AddDays(-1).ToString("yyyy-MM-dd"),
                            "/",
                           "kyfw.12306.cn")
            );

            HttpWebRequestExtension.cookieList.Remove("_jc_save_wfdc_flag");
            HttpWebRequestExtension.cookieList.Add("_jc_save_wfdc_flag",
                 new Cookie(
                    "_jc_save_wfdc_flag",
                    "dc",
                    "/",
                    "kyfw.12306.cn")
            );

            HttpWebRequestExtension._12306Cookies = new CookieContainer();
            foreach (Cookie item in HttpWebRequestExtension.cookieList.Values)
            {
                HttpWebRequestExtension._12306Cookies.Add(new Uri("https://kyfw.12306.cn" + item.Path), item);
            }
        }

        /// <summary>
        /// 进入最后选人和验证码的界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            RobTicket finalStep = new RobTicket();
            finalStep.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 点击行中的预定按钮，直接进入选票界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {
                TicketBiz.secretStr = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                TicketBiz.stationTrainCode = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                TicketBiz.train_no = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                TicketBiz.fromStationTelecode = this.cmbstartStation.SelectedValue.ToString();
                TicketBiz.toStationTelecode = this.cmbendStation.SelectedValue.ToString();
                TicketBiz.train_date = this.dtpTrainDate.Value.ToUniversalTime().ToString();
                TicketBiz.query_from_station_name = this.cmbstartStation.Text;
                TicketBiz.query_to_station_name = this.cmbendStation.Text;
                SetPassenger();
                button2_Click(sender, e);
            }
        }

        /// <summary>
        /// 将界面选好的人和席别，拼凑好写入静态变量，以供后面的修改
        /// </summary>
        private void SetPassenger()
        {
            StringBuilder sbGetPassenger = new StringBuilder();
            StringBuilder sbOldPassgener = new StringBuilder();
            foreach (var item in this.checkedListBox1.CheckedItems)
            {
                var type = Util.AnonymousTypeCast(item, new { Key = "", Value = "" });
                string selectedStrig = type.Value;
                sbGetPassenger.Append("O," + selectedStrig + "_");
                sbOldPassgener.Append(selectedStrig.Substring(selectedStrig.IndexOf(",", 0, 2) + 1, selectedStrig.IndexOf(",", 0, 6)) + "_");
            }
            sbGetPassenger = sbGetPassenger.Remove(sbGetPassenger.Length - 1, 1);

            TicketBiz.passengerTicketStr = sbGetPassenger.ToString();
            TicketBiz.oldPassengerStr = sbOldPassgener.ToString();
        }
    }
}
