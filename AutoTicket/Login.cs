using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTicket
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.MethodToAccessSSL();

            string URL = "https://dynamic.12306.cn/otsweb/passCodeAction.do?rand=lrand";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(URL);
            webRequest.CookieContainer = this.cc;
            WebResponse response = webRequest.GetResponse();
            Stream s = response.GetResponseStream();
            this.pictureBox1.Image = Image.FromStream(s);
            response.Close();
        }

        private CookieContainer cc = new CookieContainer();

        private void button2_Click(object sender, EventArgs e)
        {
            this.richTextBox1.Clear();
            this.tryLogin();
        }

        private void tryLogin()
        {
            string content = this.login();
            Regex reg = new Regex("当前??用??多，?稍后重?!");
            if (reg.Match(content).Success)
            {
                this.tryLogin();
            }
            else
            {
                this.richTextBox1.Text = content;
            }
        }

        private string login()
        {
            //this.MethodToAccessSSL();

            string url = "https://dynamic.12306.cn/otsweb/loginAction.do?method=login";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Accept = "image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
            req.Referer = "https://dynamic.12306.cn/otsweb/loginAction.do?method=init";
            req.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E; BRI/2; InfoPath.2; BO1IE8_v1;ENUS)";
            req.Host = "dynamic.12306.cn";
            req.KeepAlive = true;
            req.CookieContainer = cc;
            byte[] sendData = null;
            sendData = Encoding.Default.GetBytes("loginUser.user_name=xxxx&nameErrorFocus=&user.password=Xxxxxx&passwordErrorFocus=&randCode=" + this.txtUserName.Text + "&randErrorFocus=");
            req.ContentLength = sendData.Length;
            Stream reqStream = req.GetRequestStream();
            reqStream.Write(sendData, 0, sendData.Length);
            reqStream.Close();
            WebResponse res = req.GetResponse();
            Stream s = res.GetResponseStream();
            string content = new StreamReader(s).ReadToEnd();

            //this.webBrowser1.DocumentText = content;
            res.Close();
            return content;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                button3_Click(sender, e);
            }
            catch
            {
                CDNReset.GetCDN();
                Form1_Load(sender, e);
            }
        }

        /// <summary>
        /// 登录的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click_1(object sender, EventArgs e)
        {
            //var loginRes = HttpWebRequestExtension.GetWebContent(FirstLoginGetCookieUrl, null);
            this.richTextBox1.Text = "";

            var checkRandCode = TicketBiz.FirstCheckRandCode(txtRandCode.Text);
            dynamic checkResultJson = JsonConvert.DeserializeObject(checkRandCode);
            this.richTextBox1.Text += (checkResultJson["data"]["msg"].ToString() == "TRUE" ? "检查验证码成功" : "检查失败")+ Environment.NewLine;

            var loginRes = TicketBiz.FirstLogin(txtUserName.Text, txtPassword.Text, txtRandCode.Text);
            dynamic data = JsonConvert.DeserializeObject(loginRes);


            try
            {
                this.richTextBox1.Text += (data["data"]["loginCheck"].ToString() == "Y" ? "登录成功" : "登录失败!") + Environment.NewLine;
            }
            catch
            {
                this.richTextBox1.Text += data["messages"].ToString() + Environment.NewLine;    
            }

            TicketBiz.LoginFinalStep();
        }

        /// <summary>
        /// 打开查询车次的代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click_1(object sender, EventArgs e)
        {
            QueryTicket tic = new QueryTicket();
            tic.Show();
        }

        /// <summary>
        /// 看不清图片，刷新一张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            this.txtRandCode.Text = "";
            this.richTextBox1.Text = "";
            var random = string.Format(TrainUrlConstant.loginImg, new Random().NextDouble());
            this.pictureBox1.Image = Image.FromStream(HttpWebRequestExtension.GetWebImage(random, new CookieContainer()));
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
    }
}
