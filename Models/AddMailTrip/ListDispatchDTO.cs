using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class ListDispatchDTO
    {
        public int PostBagIndex { get; set; }
        public int FromPOSCode { get; set; }
        public int ToPOSCode { get; set; }
        public string MailTripType { get; set; }
        public string ServiceCode { get; set; }
        public int Year { get; set; }
        public int MailTripNumber { get; set; }
        public string ItemCode { get; set; }
        public int Status { get; set; }
        public int IndexNumber { get; set; }
        public int Sheet { get; set; }
        public int DeliveryRouteCode { get; set; }
        public string CounterCode { get; set; }
        public string ShiftCode { get; set; }
    }
}
