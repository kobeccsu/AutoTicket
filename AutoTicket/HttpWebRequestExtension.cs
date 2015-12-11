using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutoTicket
{
    /// <summary>
    /// 模拟网页操作,提交、获取订单页面数据
    /// </summary>
    public class HttpWebRequestExtension
    {
        public static string contentType = "application/x-www-form-urlencoded";
        public static string accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
        public static string userAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        public static string referer = "https://kyfw.12306.cn/";
        public static CookieContainer _12306Cookies = new CookieContainer();
        private static bool UserProxy = true; // 公司网络有时候要开启代理
        /// <summary>
        /// 后续提交步骤所需要的令牌
        /// </summary>
        public static string TOKEN = "";

        /// <summary>
        /// 提交订单数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string PostWebContent(string url, CookieContainer cookie, string param)
        {
            byte[] bs = Encoding.ASCII.GetBytes(param);
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookie;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "POST";
            if (UserProxy)
            {
                httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8087); // 由于公司 ip 被封，用了google 代理 
            }
            httpWebRequest.ContentLength = bs.Length;
            httpWebRequest.ServicePoint.Expect100Continue = false;
            using (Stream reqStream = httpWebRequest.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            fixCookies(httpWebRequest, (HttpWebResponse)httpWebResponse);
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            httpWebRequest.Abort();
            httpWebResponse.Close();

            return html;
        }

        /// <summary>
        /// 获取页面数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string GetWebContent(string url, CookieContainer cookie)
        {
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Referer = referer;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "GET";

            if (UserProxy)
            {
                httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8087); 
            }
            httpWebRequest.ServicePoint.ConnectionLimit = int.MaxValue;

            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            fixCookies(httpWebRequest, (HttpWebResponse)httpWebResponse);
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            
            string html = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            httpWebRequest.Abort();
            httpWebResponse.Close();

            return html;
        }

        /// <summary>
        /// 获取网页验证码图片
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static Stream GetWebImage(string url, CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Referer = referer;
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            request.Accept = "image/webp,*/*;q=0.8";
            request.CookieContainer = cookie;
            //request.ContentType = contentType;
            request.KeepAlive = true;
            //request.UseDefaultCredentials = true;

            if (UserProxy)
            {
                request.Proxy = new WebProxy("127.0.0.1", 8087); 
            }
            WebResponse response = request.GetResponse();
            //response.ResponseUri
            fixCookies(request, (HttpWebResponse)response);
            return response.GetResponseStream();
        }

        /// <summary>
        /// 获取 cookie，因为在获取第一张图片的时候才可以知道，而且这个时候只放在 Set-Cookie 这个节里，要把他读出来
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        private static void fixCookies(HttpWebRequest request, HttpWebResponse response)
        {
            for (int i = 0; i < response.Headers.Count; i++)
            {
                string name = response.Headers.GetKey(i);
                if (name != "Set-Cookie")
                {
                    continue;
                }
                string value = response.Headers.Get(i);
                foreach (var singleCookie in value.Split(','))
                {
                    var path = singleCookie.Split(';')[1].Split('=')[1];
                    Match match = Regex.Match(singleCookie, "(.*?)=(.*?);");
                    if (match.Captures.Count == 0)
                        continue;
                    _12306Cookies.Add(
                        new Cookie(
                            match.Groups[1].ToString(),
                            match.Groups[2].ToString(),
                            path,
                            request.Host.Split(':')[0]));
                    response.Cookies.Add(new Cookie(
                            match.Groups[1].ToString(),
                            match.Groups[2].ToString(),
                            path,
                            request.Host.Split(':')[0]));
                }
            }
        }

        /// <summary>
        /// 获取提交订票时所需要的 token
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string GetToken(string inputString)
        {
            Match match = Regex.Match(inputString, "(?<=()var globalRepeatSubmitToken = ').*(?=';)");
            if (match.Success)
            {
                return match.Value;
            }
            return string.Empty;
        }
    }
}
