using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket
{
    public static class Util
    {
        public static void ClassToDataRow<T>(DataTable table, T classObject) where T : class
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                //if (IsColumnByNameInvalid(table.Columns, property.Name)) { continue; }
                if (!table.Columns.Contains(property.Name))
                {
                    table.Columns.Add(property.Name);
                }
            }
        }

        public static void ClassToField<T>(DataTable table, DataRow row, T classObject) where T : class
        {
            //bool rowChanged = false;
            //DataRow row = table.NewRow();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                //if (IsColumnByNameInvalid(table.Columns, property.Name)) { continue; }

                //rowChanged = true;
                row[property.Name] = property.GetValue(classObject, null);

            }

            //if (!rowChanged) { return; }
            //table.Rows.Add(row);
        } 

        private static bool IsColumnByNameInvalid(DataColumnCollection columns, string propertyName)
        {
            return !columns.Contains(propertyName) || columns[propertyName] == null;
        }

        /// <summary>
        /// 于 javascript 的 escape 相同的后台方法
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Escape(string s)
        {
            StringBuilder sb = new StringBuilder();
            byte[] ba = System.Text.Encoding.Unicode.GetBytes(s);
            for (int i = 0; i < ba.Length; i += 2)
            {
                if (ba[i + 1] == 0)
                {
                    //数字,大小写字母,以及"+-*/._"不变
                    if (
                          (ba[i] >= 48 && ba[i] <= 57)
                        || (ba[i] >= 64 && ba[i] <= 90)
                        || (ba[i] >= 97 && ba[i] <= 122)
                        || (ba[i] == 42 || ba[i] == 43 || ba[i] == 45 || ba[i] == 46 || ba[i] == 47 || ba[i] == 95)
                        )//保持不变
                    {
                        sb.Append(Encoding.Unicode.GetString(ba, i, 2));

                    }
                    else//%xx形式
                    {
                        sb.Append("%");
                        sb.Append(ba[i].ToString("X2"));
                    }
                }
                else
                {
                    sb.Append("%u");
                    sb.Append(ba[i + 1].ToString("X2"));
                    sb.Append(ba[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }

        ///
        /// 使用WebRequest?接之前?用此方法就可以了.
        ///
        public static void MethodToAccessSSL()
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

        /// <summary>
        /// 遍历CookieContainer
        /// </summary>
        /// <param name="cc"></param>
        /// <returns></returns>
        public static bool CookieContainsValueName(this CookieContainer cookies, string cookieName)
        {
            //List<Cookie> lstCookies = new List<Cookie>();

            //Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
            //    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
            //    System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            //foreach (object pathList in table.Values)
            //{
            //    SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
            //        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
            //        | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
            //    foreach (CookieCollection colCookies in lstCookieCol.Values)
            //        foreach (Cookie c in colCookies) lstCookies.Add(c);
            //}

            BugFix_CookieDomain(cookies);

            Hashtable table = (Hashtable)cookies.GetType().InvokeMember("m_domainTable",
                                                                         BindingFlags.NonPublic |
                                                                         BindingFlags.GetField |
                                                                         BindingFlags.Instance,
                                                                         null,
                                                                         cookies,
                                                                         new object[] { });



            foreach (var key in table.Keys)
            {
                if (!key.ToString().StartsWith("."))
                {
                    foreach (Cookie cookie in cookies.GetCookies(new Uri(string.Format("https://{0}",
                        key))))
                    {
                        if (cookie.Name.Trim() == cookieName.Trim()) return true;
                        //Console.WriteLine("Name = {0} ; Value = {1} ; Domain = {2}", cookie.Name, cookie.Value,
                        //                  cookie.Domain);
                    }
                }
            }
            return false;
        }

        public static void BugFix_CookieDomain(CookieContainer cookieContainer)
        {
            Hashtable table = (Hashtable)cookieContainer.GetType().InvokeMember("m_domainTable",
            System.Reflection.BindingFlags.NonPublic |
            System.Reflection.BindingFlags.GetField |
            System.Reflection.BindingFlags.Instance,
            null,
            cookieContainer,
            new object[] { });
            ArrayList keys = new ArrayList(table.Keys);
            foreach (string keyObj in keys)
            {
                string key = (keyObj as string);
                if (key[0] == '.')
                {
                    string newKey = key.Remove(0, 1);
                    table[newKey] = table[keyObj];
                }
            }
        }
    }
}
