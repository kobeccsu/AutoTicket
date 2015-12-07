using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 *  CDN 2015年12月7日 扫描出来的列表
60.211.208.225 kyfw.12306.cn
60.210.18.12 kyfw.12306.cn
111.11.152.225 kyfw.12306.cn
117.156.21.51 kyfw.12306.cn
111.1.53.220 kyfw.12306.cn
111.47.244.240 kyfw.12306.cn
112.25.35.61 kyfw.12306.cn
112.25.35.79 kyfw.12306.cn
182.140.130.51 kyfw.12306.cn
222.211.64.51 kyfw.12306.cn
121.251.255.237 kyfw.12306.cn
222.216.188.89 kyfw.12306.cn
222.216.188.94 kyfw.12306.cn
222.23.55.208 kyfw.12306.cn
222.23.55.208 kyfw.12306.cn
210.39.4.18 kyfw.12306.cn
210.39.4.17 kyfw.12306.cn
222.23.55.208 kyfw.12306.cn
42.157.0.222 kyfw.12306.cn
113.107.57.43 kyfw.12306.cn
125.90.204.122 kyfw.12306.cn
113.107.112.214 kyfw.12306.cn
14.215.231.173 kyfw.12306.cn	
58.216.21.93 kyfw.12306.cn
 */
namespace AutoTicket
{
    /// <summary>
    /// 由于我们经常读到的是死票，所以需要切换 CDN
    /// </summary>
    public class CDNReset
    {
        public static string hostPath = @"C:\Windows\System32\Drivers\etc\hosts";
        public static List<string> lines = File.ReadAllLines(hostPath).Where(l => l.Contains("kyfw.12306.cn")).Select(l => l.StartsWith("#") ? l : "#" + l).ToList();
        public static List<string> otherlines = File.ReadAllLines(hostPath).Where(l => !l.Contains("kyfw.12306.cn")).ToList();
        public static int i = 0;
        public static int s = 2800;

        /// <summary>
        /// 返回本地 hosts 文件中已经 ping 到的 12306 cdn
        /// </summary>
        /// <returns></returns>
        public static string GetCDN()
        {
            var newlines = lines.Select(l => l).ToList();
            newlines[i] = newlines[i].Substring(1);

            var newline = newlines[i];

            var alllines = otherlines.Union(newlines).ToList();
            File.WriteAllLines(hostPath, alllines);

            if (i == lines.Count - 1)
            {
                i = 0;
            }
            else
            {
                i = i + 1;
            }

            return newline;
        }
    }
}
