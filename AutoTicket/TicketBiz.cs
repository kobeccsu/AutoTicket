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
        /// 首次登陆检查验证码
        /// </summary>
        /// <returns></returns>
        public static string FirstCheckRandCode(string randCode)
        {
            var checkRandCode = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckRand, HttpWebRequestExtension._12306Cookies,
                "randCode=" + randCode + "&rand=sjrand");
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
                              "loginUserDTO.user_name=" + userName + "&userDTO.password=" + password
                              + "&randCode=" + randCode
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
            var param = "secretStr=" + "MjAxNS0xMi0xOCMwMCNHMTAxOCMwMjo1MCMxNToxNSM2aTAwMEcxMDE4MDIjSU9RI0hWUSMxODowNSPmt7HlnLPljJcj6KGh6Ziz5LicIzAxIzA3I08wMzE4NTAxMTdNMDQ4ODUwMDQwOTA5NjM1MDAwNCNROSMxNDUwMzM1MDAwODczIzE0NDUzMDI4MDAwMDAjMjBBQ0E5OTMxRjY0QTFCNTBFMDM0RjQzRTNDODQ5QUE1QUE2QjhEQjI4MTEyNTBFQjdDNDU1MDM%3D"
                + "&train_date=" + "2015-12-19" +
                "&back_train_date=" + "2015-12-18" + "&tour_flag=dc&purpose_codes=ADULT&query_from_station_name="
                + WebUtility.UrlEncode("深圳") + "&query_to_station_name=" + WebUtility.UrlEncode("衡阳") + "&undefined=";

            HttpWebRequestExtension.referer = "https://kyfw.12306.cn/otn/leftTicket/init";
            HttpWebRequestExtension.contentType = "application/json;charset=UTF-8";
            return HttpWebRequestExtension.PostWebContent(TrainUrlConstant.SubmitOrderPredicateUrl, HttpWebRequestExtension._12306Cookies, param);
        }

        /// <summary>
        /// 获取好后续步骤需要的 Token 然后获取联系人信息
        /// </summary>
        /// <returns></returns>
        public static string GetTokenThenGetPassenger()
        {
            var redirectInitDC = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.InitDcPage, HttpWebRequestExtension._12306Cookies, "_json_att=");
            HttpWebRequestExtension.TOKEN = HttpWebRequestExtension.GetToken(redirectInitDC);

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
            var result = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.LastCheckRandCode, HttpWebRequestExtension._12306Cookies, "randCode=" + randCode +
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
            var checkOrderResult = HttpWebRequestExtension.PostWebContent(TrainUrlConstant.CheckOrderInfo, HttpWebRequestExtension._12306Cookies,
                "cancel_flag=2" +
        "&bed_level_order_num=000000000000000000000000000000" +
        "&passengerTicketStr=" + Util.Escape("O,0,1,周磊,1,430403198512142019,15820752123,N_O,0,1,何昭慧,1,430482198612030060,13420996107,N") +
        "&oldPassengerStr=" + Util.Escape("周磊,1,430403198512142019,1_何昭慧,1,430482198612030060,1_") +
        "&tour_flag=dc" +
        "&randCode=" + randCode +
        "&_json_att=" +
        "&REPEAT_SUBMIT_TOKEN=" + HttpWebRequestExtension.TOKEN, PostParamSet.NoCache);
            return checkOrderResult;
        }
    }
}
