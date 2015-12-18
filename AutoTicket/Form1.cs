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
    public partial class Form1 : Form
    {
        private static string FirstLoginGetCookieUrl = "https://kyfw.12306.cn/otn/resources/merged/common_js.js?scriptVersion=1.8840";

        public Form1()
        {
            InitializeComponent();
        }

        ///
        /// 使用WebRequest?接之前?用此方法就可以了.
        ///
        private void MethodToAccessSSL()
        {
            // 
            ServicePointManager.ServerCertificateValidationCallback =
                 new RemoteCertificateValidationCallback(ValidateServerCertificate);
            //WebRequest myRequest = WebRequest.Create(url); 
        }

        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.MethodToAccessSSL();

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
            this.MethodToAccessSSL();

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
            button3_Click(sender, e);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //var loginRes = HttpWebRequestExtension.GetWebContent(FirstLoginGetCookieUrl, null);
            //this.richTextBox1.Text = loginRes;

            var checkRandCode = TicketBiz.FirstCheckRandCode(txtRandCode.Text);
            this.richTextBox1.Text += checkRandCode;

            var loginRes = TicketBiz.FirstLogin(txtUserName.Text, txtPassword.Text, txtRandCode.Text);

            TicketBiz.LoginFinalStep();

            this.richTextBox1.Text += loginRes;
        }

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
