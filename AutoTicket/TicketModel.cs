using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket
{
    /// <summary>
    /// 查询出的余票每条的结果
    /// </summary>
    public class QueryLeftNewDTO
    {
        public string train_no { get; set; }
        public string station_train_code { get; set; }
        public string start_station_telecode { get; set; }
        public string start_station_name { get; set; }
        public string end_station_telecode { get; set; }
        public string end_station_name { get; set; }
        public string from_station_telecode { get; set; }
        public string from_station_name { get; set; }
        public string to_station_telecode { get; set; }
        public string to_station_name { get; set; }
        public string start_time { get; set; }
        public string arrive_time { get; set; }
        public string day_difference { get; set; }
        public string train_class_name { get; set; }
        public string lishi { get; set; }
        public string canWebBuy { get; set; }
        public string lishiValue { get; set; }
        public string yp_info { get; set; }
        public string control_train_day { get; set; }
        public string start_train_date { get; set; }
        public string seat_feature { get; set; }
        public string yp_ex { get; set; }
        public string train_seat_feature { get; set; }
        public string seat_types { get; set; }
        public string location_code { get; set; }
        public string from_station_no { get; set; }
        public string to_station_no { get; set; }
        public int control_day { get; set; }
        public string sale_time { get; set; }
        public string is_support_card { get; set; }
        public string controlled_train_flag { get; set; }
        public string controlled_train_message { get; set; }
        /// <summary>
        /// 硬座
        /// </summary>
        public string yz_num { get; set; }
        /// <summary>
        /// 软座
        /// </summary>
        public string rz_num { get; set; }
        
        /// <summary>
        /// 硬卧
        /// </summary>
        public string yw_num { get; set; }
        /// <summary>
        /// 软卧
        /// </summary>
        public string rw_num { get; set; }
        public string gr_num { get; set; }
        /// <summary>
        /// 一等座
        /// </summary>
        public string zy_num { get; set; }
        /// <summary>
        /// 二等座
        /// </summary>
        public string ze_num { get; set; }
        public string tz_num { get; set; }
        public string gg_num { get; set; }
        public string yb_num { get; set; }
        /// <summary>
        /// 无座
        /// </summary>
        public string wz_num { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qt_num { get; set; }
        /// <summary>
        /// 商务座数量
        /// </summary>
        public string swz_num { get; set; }
    }

    public class Datum
    {
        public QueryLeftNewDTO queryLeftNewDTO { get; set; }
        public string secretStr { get; set; }
        public string buttonTextInfo { get; set; }
    }

    public class ValidateMessages
    {
    }

    public class Data
    {
        public List<string> result { get; set; }
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
