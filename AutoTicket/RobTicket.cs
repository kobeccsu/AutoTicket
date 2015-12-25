using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTicket
{
    public partial class RobTicket : Form
    {
        public RobTicket()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 抢某张票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            var result = TicketBiz.LastCheckRandCode(txtRandCode.Text);

            this.richTextBox1.Text += "提交验证码后结果:" + result;

            HttpWebRequestExtension.showText = this.richTextBox1;
            this.richTextBox1.Text += Environment.NewLine + "提交订单后结果:" + TicketBiz.FinalSubmitOrder(txtRandCode.Text);

            this.richTextBox1.Text += Environment.NewLine + "查看还剩多少张票:" + TicketBiz.GetQueueCount();

            this.richTextBox1.Text += Environment.NewLine + "点最后一个按钮，是否会排队:" + TicketBiz.ConfirmSingleForQueue(txtRandCode.Text);
            //this.richTextBox1.Text += Environment.NewLine + "提交订单后结果:";// +checkOrderResult;
        }

        /// <summary>
        /// 看不清换一张图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            this.txtRandCode.Text = "";
            var random = string.Format(TrainUrlConstant.LastGetRandCode, new Random().NextDouble());
            HttpWebRequestExtension.accept = "image/webp,image/*,*/*;q=0.8";
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";

            this.richTextBox1.Text += Environment.NewLine + "图片 URL :" + random;
            this.pictureBox1.Image = Image.FromStream(HttpWebRequestExtension.GetWebImage(random, HttpWebRequestExtension._12306Cookies));
        }

        /// <summary>
        /// 选图片验证码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            this.txtRandCode.Text += (string.IsNullOrEmpty(this.txtRandCode.Text) ? "" : ",") + e.X + "," + e.Y;
        }

        /// <summary>
        /// 第一次加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobTicket_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text += "检查用户：" + TicketBiz.CheckUser();

            TicketBiz.ChooseTicketIntoLastStep();

            // initdc 页面会设置几个值 这个时候需要取出来
            var getPassenger = TicketBiz.GetTokenThenGetPassenger();

            this.richTextBox1.Text += Environment.NewLine + "获取联系人数据:" + getPassenger;

            button2_Click(sender, e);
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            this.txtRandCode.Text += (string.IsNullOrEmpty(this.txtRandCode.Text) ? "" : ",") + e.X + "," + e.Y;
        }
    }
}
