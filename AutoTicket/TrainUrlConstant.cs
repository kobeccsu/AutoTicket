using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket
{
    public class TrainUrlConstant
    {
        /// <summary>
        /// 登录图片
        /// </summary>
        public const string loginImg = "https://kyfw.12306.cn/passport/captcha/captcha-image?login_site=E&module=login&rand=sjrand&{0}";

       
        /// <summary>
        /// 用作正式登录，网上说没这一步是登录不了的
        /// </summary>
        public const string LoginSuccessFinal = "https://kyfw.12306.cn/otn/login/userLogin";
        /// <summary>
        /// 检查验证码
        /// </summary>
        public const string CheckRand = "https://kyfw.12306.cn/passport/captcha/captcha-check";
        /// <summary>
        /// 登录提交 form 地址
        /// </summary>
        public const string LoginPostForm = "https://kyfw.12306.cn/passport/web/login";

        /// <summary>
        /// 这里看起来是记录日志
        /// </summary>
        public const string LogLeftTicketLog = "https://kyfw.12306.cn/otn/leftTicket/log?leftTicketDTO.train_date={0}&leftTicketDTO.from_station={1}&leftTicketDTO.to_station={2}&purpose_codes=ADULT";
        

        /// <summary>
        /// 剩余票数
        /// </summary>
        public const string TrainleftTicketInfo = "https://kyfw.12306.cn/otn/leftTicket/query?leftTicketDTO.train_date={0}&leftTicketDTO.from_station={1}&leftTicketDTO.to_station={2}&purpose_codes=ADULT";
        /// <summary>
        /// 提交前的验证码
        /// </summary>
        public const string BeforeSubmitImg = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=passenger&rand=randp&{0}";
        /// <summary>
        /// 要走这个逻辑才可以
        /// </summary>
        public const string CheckUser = "https://kyfw.12306.cn/otn/login/checkUser";
        /// <summary>
        /// 点击预定时需要走的逻辑
        /// </summary>
        public const string SubmitOrderPredicateUrl = "https://kyfw.12306.cn/otn/leftTicket/submitOrderRequest";
        /// <summary>
        /// 进入最后选择席别页面
        /// </summary>
        public const string InitDcPage = "https://kyfw.12306.cn/otn/confirmPassenger/initDc?_json_att=";

        /// <summary>
        /// 获取所有的联系人
        /// </summary>
        public const string GetPassengers = "https://kyfw.12306.cn/otn/confirmPassenger/getPassengerDTOs";


        /// <summary>
        /// 最后获取 randcode
        /// Accept:image/webp,image/*,*/*;q=0.8
        //Accept-Encoding:gzip, deflate, sdch
        //Accept-Language:en,zh-CN;q=0.8,zh;q=0.6
        //Connection:keep-alive
        //Cookie:__NRF=F733ABEF9C97F7315330AC6579349EA1; JSESSIONID=0A02F00CC49F7CFAD26E42DFC9D88D7CFE26887BB0; BIGipServerotn=217055754.50210.0000; _jc_save_showIns=true; _jc_save_fromStation=%u6DF1%u5733%2CSZQ; _jc_save_toStation=%u8861%u9633%2CHYQ; _jc_save_fromDate=2015-12-10; _jc_save_toDate=2015-12-09; _jc_save_wfdc_flag=dc; current_captcha_type=Z
        //Host:kyfw.12306.cn
        //Referer:https://kyfw.12306.cn/otn/confirmPassenger/initDc
        //User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
        /// </summary>
        public const string LastGetRandCode = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=passenger&rand=randp&{0}";


        //-- 进入最后选好人输完验证码的步骤
        /// <summary>
        /// 最后检查验证码 post
        /// randCode:47,42,251,117
        ///rand:randp
        ///_json_att:
        ///REPEAT_SUBMIT_TOKEN:b855408a6b161572be0ebed01beae572
        /// </summary>
        public const string LastCheckRandCode = "https://kyfw.12306.cn/otn/passcodeNew/checkRandCodeAnsyn";

        /// <summary>
        /// 检查订单 Post
        /// Accept:application/json, text/javascript, */*; q=0.01
        //Accept-Encoding:gzip, deflate
        //Accept-Language:en,zh-CN;q=0.8,zh;q=0.6
        //Connection:keep-alive
        //Content-Length:468
        //Content-Type:application/x-www-form-urlencoded; charset=UTF-8
        //Cookie:JSESSIONID=0A02F00CC4F7BF51B50EC5867546A4837002FE4C1E; __NRF=F733ABEF9C97F7315330AC6579349EA1; BIGipServerotn=217055754.50210.0000; _jc_save_showIns=true; _jc_save_fromStation=%u6DF1%u5733%2CSZQ; _jc_save_toStation=%u8861%u9633%2CHYQ; _jc_save_fromDate=2015-12-10; _jc_save_toDate=2015-12-09; _jc_save_wfdc_flag=dc; current_captcha_type=Z
        //Host:kyfw.12306.cn
        //Origin:https://kyfw.12306.cn
        //Referer:https://kyfw.12306.cn/otn/confirmPassenger/initDc
        //User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
        //X-Requested-With:XMLHttpRequest

        //        cancel_flag:2
        //bed_level_order_num:000000000000000000000000000000
        //passengerTicketStr:O,0,1,周磊,1,430403198512142019,15820752123,N_O,0,1,何昭慧,1,430482198612030060,13420996107,N
        //oldPassengerStr:周磊,1,430403198512142019,1_何昭慧,1,430482198612030060,1_
        //tour_flag:dc
        //randCode:47,42,251,117
        //_json_att:
        //REPEAT_SUBMIT_TOKEN:b855408a6b161572be0ebed01beae572

        /// </summary>
        public const string CheckOrderInfo = "https://kyfw.12306.cn/otn/confirmPassenger/checkOrderInfo";

        /// <summary>
        /// 获取排队队列
        /// Accept:application/json, text/javascript, */*; q=0.01
        /// Accept-Encoding:gzip, deflate
        /// Accept-Language:en,zh-CN;q=0.8,zh;q=0.6
        /// Connection:keep-alive
        /// Content-Length:332
        /// Content-Type:application/x-www-form-urlencoded; charset=UTF-8
        /// Cookie:JSESSIONID=0A02F00CC4F7BF51B50EC5867546A4837002FE4C1E; __NRF=F733ABEF9C97F7315330AC6579349EA1; BIGipServerotn=217055754.50210.0000; _jc_save_showIns=true; _jc_save_fromStation=%u6DF1%u5733%2CSZQ; _jc_save_toStation=%u8861%u9633%2CHYQ; _jc_save_fromDate=2015-12-10; _jc_save_toDate=2015-12-09; _jc_save_wfdc_flag=dc; current_captcha_type=Z
        /// Host:kyfw.12306.cn
        /// Origin:https://kyfw.12306.cn
        /// Referer:https://kyfw.12306.cn/otn/confirmPassenger/initDc
        /// User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
        /// X-Requested-With:XMLHttpRequest
        /// 
        /// 
        /// 
        /// train_date:Thu Dec 10 2015 00:00:00 GMT+0800 (中国标准时间)
        /// train_no:6i000G103200
        /// stationTrainCode:G1032
        /// seatType:O
        /// fromStationTelecode:IOQ
        /// toStationTelecode:HVQ
        /// leftTicket:O031850176M0488500119096350000
        //             O000000450M0000000819000000004
        /// purpose_codes:00
        /// _json_att:
        /// REPEAT_SUBMIT_TOKEN:b855408a6b161572be0ebed01beae572
        /// </summary>
        public const string GetQueueCount = "https://kyfw.12306.cn/otn/confirmPassenger/getQueueCount";


        /// <summary>
        /// 当出来票的确认信息时点击确定
        /// Accept:application/json, text/javascript, */*; q=0.01
        // Accept-Encoding:gzip, deflate
        // Accept-Language:en,zh-CN;q=0.8,zh;q=0.6
        //Connection:keep-alive
        //Content-Length:566
        //Content-Type:application/x-www-form-urlencoded; charset=UTF-8
        //Cookie:JSESSIONID=0A02F00CC4F7BF51B50EC5867546A4837002FE4C1E; __NRF=F733ABEF9C97F7315330AC6579349EA1; BIGipServerotn=217055754.50210.0000; _jc_save_showIns=true; _jc_save_fromStation=%u6DF1%u5733%2CSZQ; _jc_save_toStation=%u8861%u9633%2CHYQ; _jc_save_fromDate=2015-12-10; _jc_save_toDate=2015-12-09; _jc_save_wfdc_flag=dc; current_captcha_type=Z
        //Host:kyfw.12306.cn
        //Origin:https://kyfw.12306.cn
        //Referer:https://kyfw.12306.cn/otn/confirmPassenger/initDc
        //User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
        //X-Requested-With:XMLHttpRequest
        /// 
        /// passengerTicketStr:O,0,1,周磊,1,430403198512142019,15820752123,N_O,0,1,何昭慧,1,430482198612030060,13420996107,N
        //  oldPassengerStr:周磊,1,430403198512142019,1_何昭慧,1,430482198612030060,1_
        //  randCode:47,42,251,117
        //  purpose_codes:00
        //  key_check_isChange:1AA53CDF249030F74DE9C8419B613EA2C19B569EC1044B802F7E79B6
        //  leftTicketStr:O000000176M0000000119000000000
        //  train_location:Q6
        //  roomType:00
        //  dwAll:N
        //  _json_att:
        //  REPEAT_SUBMIT_TOKEN:b855408a6b161572be0ebed01beae572
        /// 
        /// </summary>
        public const string ConfirmForSingleQueue = "https://kyfw.12306.cn/otn/confirmPassenger/confirmSingleForQueue";

        /// <summary>
        /// 最终抢票结果
        /// Accept:application/json, text/javascript, */*; q=0.01
        //Accept-Encoding:gzip, deflate
        //Accept-Language:en,zh-CN;q=0.8,zh;q=0.6
        //Connection:keep-alive
        //Content-Length:91
        //Content-Type:application/x-www-form-urlencoded; charset=UTF-8
        //Cookie:JSESSIONID=0A02F00CC4F7BF51B50EC5867546A4837002FE4C1E; __NRF=F733ABEF9C97F7315330AC6579349EA1; BIGipServerotn=217055754.50210.0000; _jc_save_showIns=true; _jc_save_fromStation=%u6DF1%u5733%2CSZQ; _jc_save_toStation=%u8861%u9633%2CHYQ; _jc_save_fromDate=2015-12-10; _jc_save_toDate=2015-12-09; _jc_save_wfdc_flag=dc; current_captcha_type=Z
        //Host:kyfw.12306.cn
        //Origin:https://kyfw.12306.cn
        //Referer:https://kyfw.12306.cn/otn/confirmPassenger/initDc
        //User-Agent:Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36
        //X-Requested-With:XMLHttpRequest
        /// 
        /// orderSequence_no:E598858670
        //_json_att:
        //REPEAT_SUBMIT_TOKEN:b855408a6b161572be0ebed01beae572
        /// </summary>
        public const string TicketFinalResult = "https://kyfw.12306.cn/otn/confirmPassenger/resultOrderForDcQueue";
    }
}
