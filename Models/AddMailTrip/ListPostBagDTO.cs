using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class ListPostBagDTO
    {
        public int PostBagIndex { get; set; }
        public string PostBagTypeCode { get; set; }
        public int F { get; set; }
        public int FromPOSCode { get; set; }
        public int ToPOSCode { get; set; }
        public string MailTripType { get; set; }
        public string ServiceCode { get; set; }
        public int Year { get; set; }
        public int MailTripNumber { get; set; }
        public int PostBagNumber { get; set; }
        public decimal Weight { get; set; }
        public int Status { get; set; }
        public int Quantity { get; set; }
        public int IsPrinted { get; set; }
        public DBNull BC37Date { get; set; }
        public DateTime PackagingTime { get; set; }
        public int PackagingUser { get; set; }
        public string PackagingMachine { get; set; }
        public string OpeningTime { get; set; }
        public string OpeningMachine { get; set; }
        public string OpeningUser { get; set; }
        public DBNull IncomingDate { get; set; }
        public int CaseWeight { get; set; }
        public int IsDiscrete { get; set; }
        public int IsDeliveryRoute { get; set; }
        public string PostBagCode { get; set; }
        public string Note { get; set; }
    }
}
