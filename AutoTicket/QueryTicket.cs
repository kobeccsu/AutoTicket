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
using System.Windows.Forms;

namespace AutoTicket
{
    public partial class QueryTicket : Form
    {
        public Dictionary<string, string> cacheDic = new Dictionary<string, string>();

        public QueryTicket()
        {
            InitializeComponent();
            GetData();
        }

        /// <summary>
        /// 加载站点间数据，这里可能已经过时，手动输入的时候已经不是这几个站点了
        /// </summary>
        private void GetData()
        {
            var url = "https://kyfw.12306.cn/otn/resources/js/framework/station_name.js";
            var loginRes = HttpWebRequestExtension.GetWebContent(url, HttpWebRequestExtension._12306Cookies);
            SplitData(loginRes);
        }

        /// <summary>
        /// 拆分12306 站点数据
        /// </summary>
        /// <param name="station"></param>
        private void SplitData(string station)
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
                        //cmbstartStation.Items.Add(statename);
                        //cmbendStation.Items.Add(statename);
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
            this.richTextBox1.Text = "";

            var url = string.Format(TrainUrlConstant.TrainleftTicketInfo, dtpTrainDate.Value.ToString("yyyy-MM-dd"),
                cmbstartStation.SelectedValue, cmbendStation.SelectedValue);

            var trainleftTicketInfoRes = HttpWebRequestExtension.GetWebContent(url, HttpWebRequestExtension._12306Cookies);

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
        }

        // 刷出的列表选车次
        private void button2_Click(object sender, EventArgs e)
        {

            RobTicket finalStep = new RobTicket();
            finalStep.Show();

        }

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.lblCDNSite.Text = CDNReset.GetCDN();
        }
    }
}
