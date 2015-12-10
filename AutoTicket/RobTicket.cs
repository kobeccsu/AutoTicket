﻿using System;
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

        private void button1_Click(object sender, EventArgs e)
        {
            var result = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.LastCheckRandCode, HttpWebRequestExtension._12306Cookies, "randCode=" + this.txtRandCode.Text+
                "&rand=randp&_json_att="+"&REPEAT_SUBMIT_TOKEN="+HttpWebRequestExtension.TOKEN);
            this.richTextBox1.Text += "提交验证码后结果:" + result;

            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            var checkOrderResult = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckOrderInfo, HttpWebRequestExtension._12306Cookies,
                "cancel_flag=2"+
        "&bed_level_order_num=000000000000000000000000000000"+
        "&passengerTicketStr=O,0,1,周磊,1,430403198512142019,15820752123,N_O,0,1,何昭慧,1,430482198612030060,13420996107,N"+
        "&oldPassengerStr=周磊,1,430403198512142019,1_何昭慧,1,430482198612030060,1_"+
        "&tour_flag=dc"+
        "&randCode="+txtRandCode.Text+
        "&_json_att="+
        "&REPEAT_SUBMIT_TOKEN="+HttpWebRequestExtension.TOKEN);
            this.richTextBox1.Text += "提交订单后结果:" + checkOrderResult;
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
            this.pictureBox1.Image = Image.FromStream((Stream)HttpWebRequestExtension.GetWebImage(random, HttpWebRequestExtension._12306Cookies));
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

        private void RobTicket_Load(object sender, EventArgs e)
        {
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            var checkuser = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckUser, HttpWebRequestExtension._12306Cookies, "_json_att=");

            var param = "secretStr=" + "MjAxNS0xMi0xMCMwMCNHMTAzMiMwMjozOSMxMjozNyM2aTAwMEcxMDMyMDAjSU9RI0hWUSMxNToxNiPmt7HlnLPljJcj6KGh6Ziz5LicIzAxIzA2I08wMDAwMDAyMTdNMDAwMDAwMDEyOTAwMDAwMDAwMCNRNiMxNDQ5NjYzNTA4OTQxIzE0NDQ2MTE2MDAwMDAjNzJFOUNDODY2MjU2OEU1MTRFRjIyOTIwREY3REQwMEZBOEZGMjU1RUIxMzYzOTMzQUNDQjY5OTY="
                + "&train_date=" + "2015-12-11" +
                "&back_train_date=" + "2015-12-10" + "&tour_flag=dc&purpose_codes=ADULT&query_from_station_name="
                + "深圳" + "&query_to_station_name=" + "衡阳" + "&undefined=";

            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            HttpWebRequestExtension.contentType = "application/json;charset=UTF-8";
            var confirmParmRes = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.SubmitOrderPredicateUrl, HttpWebRequestExtension._12306Cookies, param);

            // initdc 页面会设置几个值 这个时候需要取出来
            var redirectInitDC = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.InitDcPage, HttpWebRequestExtension._12306Cookies, "_json_att=");

            HttpWebRequestExtension.TOKEN = HttpWebRequestExtension.GetToken(redirectInitDC);
            button2_Click(sender, e);
        }

        private void pictureBox1_MouseClick_1(object sender, MouseEventArgs e)
        {
            this.txtRandCode.Text += (string.IsNullOrEmpty(this.txtRandCode.Text) ? "" : ",") + e.X + "," + e.Y;
        }
    }
}
