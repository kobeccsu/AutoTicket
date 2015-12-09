using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoTicket.JSON.Passenger
{
    public class NormalPassenger
    {
        public string code { get; set; }
        public string passenger_name { get; set; }
        public string sex_code { get; set; }
        public string sex_name { get; set; }
        public string born_date { get; set; }
        public string country_code { get; set; }
        public string passenger_id_type_code { get; set; }
        public string passenger_id_type_name { get; set; }
        public string passenger_id_no { get; set; }
        public string passenger_type { get; set; }
        public string passenger_flag { get; set; }
        public string passenger_type_name { get; set; }
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
