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
        public const string loginImg = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=login&rand=sjrand&{0}";

        /// <summary>
        /// 用作正式登录，网上说没这一步是登录不了的
        /// </summary>
        public const string LoginSuccessFinal = "https://kyfw.12306.cn/otn/login/userLogin";
        /// <summary>
        /// 检查验证码
        /// </summary>
        public const string CheckRand = "https://kyfw.12306.cn/otn/passcodeNew/checkRandCodeAnsyn";
        /// <summary>
        /// 登录提交 form 地址
        /// </summary>
        public const string LoginPostForm = "https://kyfw.12306.cn/otn/login/loginAysnSuggest";
        /// <summary>
        /// 剩余票数
        /// </summary>
        public const string TrainleftTicketInfo = "https://kyfw.12306.cn/otn/leftTicket/queryT?leftTicketDTO.train_date={0}&leftTicketDTO.from_station={1}&leftTicketDTO.to_station={2}&purpose_codes=ADULT";
        /// <summary>
        /// 提交前的验证码
        /// </summary>
        public const string BeforeSubmitImg = "https://kyfw.12306.cn/otn/passcodeNew/getPassCodeNew?module=passenger&rand=randp&{0}";

        public const string SubmitOrderPredicateUrl = "";
    }
}
