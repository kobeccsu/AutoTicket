using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Reflection;
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
        public static string contentType = "application/x-www-form-urlencoded; charset=UTF-8";
        public static string accept = "*/*";
        public static string userAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
        public static string referer = "https://kyfw.12306.cn/";
        public static CookieContainer _12306Cookies = new CookieContainer();
        private static bool UserProxy = false; // 公司网络有时候要开启代理

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
        public static string PostWebContent(string url, CookieContainer cookie, string param, PostParamSet postParam = PostParamSet.Normal, 
            CookieStatus cookeWrite = CookieStatus.Default)
        {
            Util.MethodToAccessSSL();
            byte[] bs = Encoding.ASCII.GetBytes(param);
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookie;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Host = "kyfw.12306.cn";
            httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //httpWebRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");

            if ((postParam & PostParamSet.NoCache) == PostParamSet.NoCache)
            {
                httpWebRequest.Headers.Add("Cache-Control", "no-cache");
                
            }
            if ((postParam & PostParamSet.If_modify_since) == PostParamSet.If_modify_since)
            {
                Type type = httpWebRequest.Headers.GetType();
                type.InvokeMember("AddInternal", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic, 
                    null, httpWebRequest.Headers, new object[] { "If-Modified-Since", "0" });
            }
            if (UserProxy)
            {
                httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8087); // 由于公司 ip 被封，用了google 代理 
            }
            
            httpWebRequest.UnsafeAuthenticatedConnectionSharing = true;
            httpWebRequest.ServicePoint.ConnectionLimit = int.MaxValue;
            httpWebRequest.ServicePoint.UseNagleAlgorithm = false;
            httpWebRequest.ContentLength = bs.Length;
            httpWebRequest.ServicePoint.Expect100Continue = false;
            //httpWebRequest.Connection = "Keep-Alive";

            using (Stream reqStream = httpWebRequest.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            
            var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            if (cookeWrite == CookieStatus.ResponseSetCookie)
            {
                fixCookies(httpWebRequest, (HttpWebResponse)httpWebResponse); 
            }
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            //httpWebRequest.Abort();
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
            Util.MethodToAccessSSL();
            var httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Referer = referer;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "GET";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.CookieContainer = cookie;
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
        /// 获取网页验证码图片,很奇怪用stream 获取的坐标是正确的，而用 path 就是不正确的
        /// </summary>
        /// <param name="url"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static Stream GetWebImage(string url, CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Referer = referer;
            request.UserAgent = userAgent;
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
            Util.BugFix_CookieDomain(_12306Cookies);
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
                    
                    //if(Util.GetAllCookies(_12306Cookies).Where(m=> m.Name == match.Groups[1].ToString()).Count() == 0)
                    //{
                    if(!_12306Cookies.CookieContainsValueName(match.Groups[1].ToString()))
                    { 
                        _12306Cookies.Add(
                            new Cookie(
                            match.Groups[1].ToString(),
                            match.Groups[2].ToString(),
                            path,
                            request.Host.Split(':')[0]));
                    }
                    //}
                    
                    //if(response.Cookies[match.Groups[1].ToString()] == null)
                    //{ 
                        response.Cookies.Add(new Cookie(
                            match.Groups[1].ToString(),
                            match.Groups[2].ToString(),
                            path,
                            request.Host.Split(':')[0]));
                    //}
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

        HttpWebRequest httpWebRequest = null;

        void StartWebRequest(string url, CookieContainer cookie, string param, PostParamSet postParam = PostParamSet.Normal,
            CookieStatus cookeWrite = CookieStatus.Default)
        {
            Util.MethodToAccessSSL();
            byte[] bs = Encoding.ASCII.GetBytes(param);
            httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.CookieContainer = cookie;
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Accept = accept;
            httpWebRequest.UserAgent = userAgent;
            httpWebRequest.Method = "POST";
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Host = "kyfw.12306.cn";
            httpWebRequest.Headers.Add("X-Requested-With", "XMLHttpRequest");
            //httpWebRequest.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            //httpWebRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");

            if ((postParam & PostParamSet.NoCache) == PostParamSet.NoCache)
            {
                httpWebRequest.Headers.Add("Cache-Control", "no-cache");

            }
            if ((postParam & PostParamSet.If_modify_since) == PostParamSet.If_modify_since)
            {
                Type type = httpWebRequest.Headers.GetType();
                type.InvokeMember("AddInternal", BindingFlags.Instance | BindingFlags.InvokeMethod | BindingFlags.NonPublic,
                    null, httpWebRequest.Headers, new object[] { "If-Modified-Since", "0" });
            }
            if (UserProxy)
            {
                httpWebRequest.Proxy = new WebProxy("127.0.0.1", 8087); // 由于公司 ip 被封，用了google 代理 
            }
            httpWebRequest.ContentLength = bs.Length;
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.BeginGetResponse(new AsyncCallback(FinishWebRequest), null);
        }

        void FinishWebRequest(IAsyncResult result)
        {
            WebResponse httpWebResponse = httpWebRequest.EndGetResponse(result);
            //if (cookeWrite == CookieStatus.ResponseSetCookie)
            //{
                fixCookies(httpWebRequest, (HttpWebResponse)httpWebResponse);
            //}
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();

            streamReader.Close();
            responseStream.Close();

            httpWebRequest.Abort();
            httpWebResponse.Close();

            //return html;
            // todo: 目前就是 checkorder 这个地方卡住，两个地方与别的地方有区别，第一是在 jquery 中是 async 提交。第二是，观察正确的都有一个keep-alive属性，
            //  而这个窗口出来的目前没有。
        }
    }

    [Flags]
    public enum PostParamSet
    { 
        /// <summary>
        /// 默认什么都不干
        /// </summary>
        Normal = 0,
        /// <summary>
        /// 设置不要缓存
        /// </summary>
        NoCache = 1,
        /// <summary>
        /// 来自更改
        /// </summary>
        If_modify_since = 2
    }

    /// <summary>
    /// 根据需要写 cookie 而不是每次都写
    /// </summary>
    public enum CookieStatus
    { 
        /// <summary>
        /// 默认不做
        /// </summary>
        Default = 0,
        /// <summary>
        /// 回发了 cookie
        /// </summary>
        ResponseSetCookie = 1
    }
}
