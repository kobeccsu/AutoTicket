using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket
{
    public class TicketBiz
    {
        /// <summary>
        /// 选中车次的一个码
        /// </summary>
        public static string secretStr = "";

        /// <summary>
        /// 发车时间
        /// </summary>
        public static string train_date = "";

        /// <summary>
        /// 列车编号，并不是车次
        /// </summary>
        public static string train_no = "";
        /// <summary>
        /// 车次，如 G1018
        /// </summary>
        public static string stationTrainCode = "";

        /// <summary>
        /// 出发站
        /// </summary>
        public static string fromStationTelecode = "";
        /// <summary>
        /// 到达站编码如，HIQ
        /// </summary>
        public static string toStationTelecode = "";
        /// <summary>
        /// 页面中隐藏的一个值，和 token 的获取方式一样
        /// </summary>
        public static string leftTicket = "";

        public static string key_check_isChange = "";

        public static string train_location = "";

        /// <summary>
        /// 出发站站名
        /// </summary>
        public static string query_from_station_name = "";

        /// <summary>
        /// 到达站站名
        /// </summary>
        public static string query_to_station_name = "";

        /// <summary>
        /// 拼这个字符串的逻辑在 12306是这个逻辑 ，用 _  分隔每个乘客
        /// seat_type 座位席别，
        /// ticket_type 1 成人片，2，儿童票，3 学生票， 4 残疾军人票
        /// name 乘客中文名
        /// id_type 1 为身份证
        /// id_no 身份证号码
        /// phone_no 手机号
        /// 
        /// limit_tickets[aA].seat_type + ",0," + limit_tickets[aA].ticket_type + "," + limit_tickets[aA].name + "," + limit_tickets[aA].id_type + "," + limit_tickets[aA].id_no + "," + (limit_tickets[aA].phone_no == null  ? "" : limit_tickets[aA].phone_no) + "," + (limit_tickets[aA].save_status == "" ? "N" : "Y");
        /// </summary>
        public static string passengerTicketStr = "O,0,1,周磊,1,430403198512142019,15820752123,N_O,0,1,何昭慧,1,430482198612030060,13420996107,N_O,0,1,王满秀,1,430482196507180025,,N";

        public static string oldPassengerStr = "周磊,1,430403198512142019,1_何昭慧,1,430482198612030060,1_王满秀,1,430482196507180025,1_";

        /// <summary>
        /// 首次登陆检查验证码
        /// </summary>
        /// <returns></returns>
        public static string FirstCheckRandCode(string randCode)
        {
            var checkRandCode = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckRand, HttpWebRequestExtension._12306Cookies,
                "answer=" + randCode + "&loginSite=E&rand=sjrand");
            return checkRandCode;
        }

        /// <summary>
        /// 检验用户名密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="randCode"></param>
        /// <returns></returns>
        public static string FirstLogin(string userName, string password, string randCode)
        {
            var loginRes = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.LoginPostForm, HttpWebRequestExtension._12306Cookies,
                              "username=" + userName + "&password=" + password
                              + "&appid=otn", PostParamSet.Normal, CookieStatus.ResponseSetCookie
                              );
            return loginRes;
        }

        /// <summary>
        /// 登陆后的一个动作，可能他网站里面会有动作
        /// </summary>
        public static void LoginFinalStep()
        {
            var finalLoginStep = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.LoginSuccessFinal, HttpWebRequestExtension._12306Cookies,
                "_json_att=");
        }

        public static string UAMTK()
        {
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.UAMTK, HttpWebRequestExtension._12306Cookies, "appid=otn&_json_att=");
        }

        public static void UAMTK_Client(string tk)
        {
            var result = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.UAMTK_Client, HttpWebRequestExtension._12306Cookies, 
                "tk=" + tk, PostParamSet.Normal, CookieStatus.ResponseSetCookie);
        }


        /// <summary>
        /// 订票流程，检查用户信息
        /// </summary>
        public static string CheckUser()
        {
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckUser, HttpWebRequestExtension._12306Cookies, "_json_att=", 
                PostParamSet.NoCache | PostParamSet.If_modify_since);
        }

        /// <summary>
        /// 选择好车票后，提交进入最后选择人和验证码的页面
        /// </summary>
        public static string ChooseTicketIntoLastStep()
        {
            // 这里这个码是动态生成的，不一样
            var param = "secretStr=" + secretStr
                + "&train_date=" + DateTime.Parse(train_date).ToString("yyyy-MM-dd") +
                "&back_train_date=" + "2015-12-23" + "&tour_flag=dc&purpose_codes=ADULT&query_from_station_name="
                + query_from_station_name + "&query_to_station_name=" + query_to_station_name + "&undefined=";

            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.SubmitOrderPredicateUrl, HttpWebRequestExtension._12306Cookies, param, PostParamSet.NoCache);
        }

        /// <summary>
        /// 获取好后续步骤需要的 Token 然后获取联系人信息
        /// </summary>
        /// <returns></returns>
        public static string GetTokenThenGetPassenger()
        {
            var redirectInitDC = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.InitDcPage, HttpWebRequestExtension._12306Cookies, "_json_att=");
            HttpWebRequestExtension.TOKEN = HttpWebRequestExtension.GetToken(redirectInitDC);
            TicketBiz.leftTicket = HttpWebRequestExtension.GetLeftTicketStr(redirectInitDC);
            TicketBiz.key_check_isChange = HttpWebRequestExtension.GetValueFromPage(redirectInitDC, "(?<='key_check_isChange':').*?(?=',)");
            TicketBiz.train_location = HttpWebRequestExtension.GetValueFromPage(redirectInitDC, "(?<=''train_location':').*?(?='})");

            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            var getPassenger = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.GetPassengers, HttpWebRequestExtension._12306Cookies, "_json_att=&REPEAT_SUBMIT_TOKEN="
                + HttpWebRequestExtension.TOKEN, PostParamSet.NoCache);
            return getPassenger;
        }

        /// <summary>
        /// 最后校验验证码
        /// </summary>
        /// <returns></returns>
        public static string LastCheckRandCode(string randCode)
        {
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            var result = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.LastCheckRandCode, HttpWebRequestExtension._12306Cookies, 
                "randCode=" + WebUtility.UrlEncode(randCode) +
                "&rand=randp&_json_att=" + "&REPEAT_SUBMIT_TOKEN=" + HttpWebRequestExtension.TOKEN, PostParamSet.NoCache);
            return result;
        }

        /// <summary>
        /// 最后提交订单
        /// </summary>
        /// <param name="randCode"></param>
        /// <returns></returns>
        public static string FinalSubmitOrder(string randCode)
        {
            HttpWebRequestExtension.accept = "application/json, text/javascript, */*; q=0.01";
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckOrderInfo, HttpWebRequestExtension._12306Cookies,
                "cancel_flag=2" +
        "&bed_level_order_num=000000000000000000000000000000" +
        "&passengerTicketStr=" + WebUtility.UrlEncode(passengerTicketStr) +
        "&oldPassengerStr=" + WebUtility.UrlEncode(oldPassengerStr) +
        "&tour_flag=dc" +
        "&randCode=" + WebUtility.UrlEncode(randCode) +
        "&_json_att=" +
        "&REPEAT_SUBMIT_TOKEN=" + HttpWebRequestExtension.TOKEN, PostParamSet.NoCache);
        }

        /// <summary>
        /// 获取剩余多少张票
        /// </summary>
        /// <returns></returns>
        public static string GetQueueCount()
        {
            HttpWebRequestExtension.accept = "application/json, text/javascript, */*; q=0.01";
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.GetQueueCount, HttpWebRequestExtension._12306Cookies,
                "train_date=" + train_date +
                "&train_no=" + train_no +
                "&stationTrainCode=" + stationTrainCode +
                "&seatType=" + "O" + // 二等座，
                "&fromStationTelecode=" + fromStationTelecode +
                "&toStationTelecode=" + toStationTelecode +
                "&leftTicket=" + leftTicket +
                "&purpose_codes=" + "00" +
                "&_json_att=" +
                "&REPEAT_SUBMIT_TOKEN=" + HttpWebRequestExtension.TOKEN
            );
        }

        /// <summary>
        /// 最后一个点击确定的按钮
        /// </summary>
        /// <returns></returns>
        public static string ConfirmSingleForQueue(string randCode)
        {
            HttpWebRequestExtension.accept = "application/json, text/javascript, */*; q=0.01";
            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/confirmPassenger/initDc";
            HttpWebRequestExtension.contentType = "application/x-www-form-urlencoded; charset=UTF-8";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.ConfirmForSingleQueue, HttpWebRequestExtension._12306Cookies,
                "passengerTicketStr=" + WebUtility.UrlEncode(passengerTicketStr) +
                "&oldPassengerStr=" + WebUtility.UrlEncode(oldPassengerStr) +
                "&randCode=" + WebUtility.UrlEncode(randCode) +
                "&purpose_codes=00" +
                "&key_check_isChange=" + key_check_isChange +
                "&leftTicketStr=" + leftTicket +
                "&train_location=" + train_location +
                "&roomType=00" +
                "&dwAll=N" +
                "&_json_att=" +
                "&REPEAT_SUBMIT_TOKEN=" + HttpWebRequestExtension.TOKEN
            );
        }
    }
}