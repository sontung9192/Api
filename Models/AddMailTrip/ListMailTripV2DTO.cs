using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class ListMailTripV2DTO
    {
        public int StartingCode { get; set; }
        public int DestinationCode { get; set; }
        public string MailtripType { get; set; }
        public string ServiceCode { get; set; }
        public int Year { get; set; }
        public int MailtripNumber { get; set; }
        public DateTime OutgoingDate { get; set; }
        public int Status { get; set; }
        public string MailRouteCode { get; set; }
        public string BC37Number { get; set; }
        public Nullable<int> IncomingDate { get; set; }
        public int Quantity { get; set; }
        public decimal Weight { get; set; }
        public int NumberItemPerSheet { get; set; }
        public DateTime PackagingTime { get; set; }
        public int PackagingUser { get; set; }
        public string PackagingMachineName { get; set; }
        public Nullable<int> OpeningTime { get; set; }
        public string OpeningUser { get; set; }
        public string OpeningMachineName { get; set; }
        public DateTime InitialTime { get; set; }
        public string InitialMachineName { get; set; }
        public int InitialUser { get; set; }
        public Nullable<int> TrasferTime { get; set; }
        public string TransferMachine { get; set; }
        public string TransferUser { get; set; }
        public string TransportNumber { get; set; }
        public string TransportCode { get; set; }
        public string OriginalTransportPOSCode { get; set; }
        public string TransportDate { get; set; }
        public string CounterCode { get; set; }
        public int DeliveryRoute { get; set; }
        public int Type { get; set; }
        public string TransferPOSCode { get; set; }
        public Nullable<int> TransferDate { get; set; }
        public int TransferStatus { get; set; }
        public int TransferTimes { get; set; }
        public string TransferID { get; set; }
    }
}
