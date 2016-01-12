using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket.JSON.Passenger
{
    /// <summary>
    /// 联系人信息   O 表示二等座， M 表示一等座, P  特等座
    /// 3 硬卧  1  硬座   4  软卧
    /// </summary>
    public class NormalPassenger
    {
        public string code { get; set; }
        /// <summary>
        /// 乘客姓名
        /// </summary>
        public string passenger_name { get; set; }
        /// <summary>
        /// 性别代码
        /// </summary>
        public string sex_code { get; set; }
        /// <summary>
        /// 性别 如 男
        /// </summary>
        public string sex_name { get; set; }
        public string born_date { get; set; }
        public string country_code { get; set; }
        public string passenger_id_type_code { get; set; }
        public string passenger_id_type_name { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string passenger_id_no { get; set; }
        public string passenger_type { get; set; }
        public string passenger_flag { get; set; }
        public string passenger_type_name { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string mobile_no { get; set; }
        public string phone_no { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string postalcode { get; set; }
        public string first_letter { get; set; }
        public string recordCount { get; set; }
        public string total_times { get; set; }
        public string index_id { get; set; }
    }

    public class Data
    {
        public bool isExist { get; set; }
        public string exMsg { get; set; }
        public List<string> two_isOpenClick { get; set; }
        public List<string> other_isOpenClick { get; set; }
        public List<NormalPassenger> normal_passengers { get; set; }
        public List<object> dj_passengers { get; set; }
    }

    public class ValidateMessages
    {
    }

    public class RootObject
    {
        public string validateMessagesShowId { get; set; }
        public bool status { get; set; }
        public int httpstatus { get; set; }
        public Data data { get; set; }
        public List<object> messages { get; set; }
        public ValidateMessages validateMessages { get; set; }
    }
}
