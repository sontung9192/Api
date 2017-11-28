using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class E2_BD13_DIDTO
    {
        public decimal MABC { get; set; }
        public decimal CHTHU { get; set; }
        public decimal NGAY { get; set; }
        public decimal SL { get; set; }
    }
    public class ListMailTripDTO
    {
        public decimal StartingCode { get; set; }
        public decimal DestinationCode { get; set; }
        public string MailtripType { get; set; }
        public string ServiceCode { get; set; }
        public decimal Year { get; set; }
        public decimal MailtripNumber { get; set; }
        public DateTime OutgoingDate { get; set; }
        public int Status { get; set; }
        public string MailRouteCode { get; set; }
        public string BC37Number { get; set; }
        public Nullable<decimal> IncomingDate { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public int NumberItemPerSheet { get; set; }
        public DateTime PackagingTime { get; set; }
        public decimal PackagingUser { get; set; }
        public string PackagingMachineName { get; set; }
        public Nullable<decimal> OpeningTime { get; set; }
        public string OpeningUser { get; set; }
        public string OpeningMachineName { get; set; }
        public DateTime InitialTime { get; set; }
        public string InitialMachineName { get; set; }
        public decimal InitialUser { get; set; }
        public Nullable<decimal> TrasferTime { get; set; }
        public string TransferMachine { get; set; }
        public string TransferUser { get; set; }
        public string TransportNumber { get; set; }
        public string TransportCode { get; set; }
        public string OriginalTransportPOSCode { get; set; }
        public string TransportDate { get; set; }
        public string CounterCode { get; set; }
        public string DeliveryRoute { get; set; }
        public int Type { get; set; }
        public string TransferPOSCode { get; set; }
        public Nullable<Decimal>  TransferDate { get; set; }
        public int TransferStatus { get; set; }
        public int TransferTimes { get; set; }
        public string TransferID { get; set; }

    }
}
