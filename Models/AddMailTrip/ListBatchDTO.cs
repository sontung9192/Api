using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class ListBatchDTO
    {
        public string BatchCode { get; set; }
        public int POSCode { get; set; }
        public string CustomerCode { get; set; }
        public int MainFreight { get; set; }
        public int Discount { get; set; }
        public int Abatement { get; set; }
        public int TotalFreight { get; set; }
        public string OrderCode { get; set; }
        public int InvoiceAttached { get; set; }
        public int OtherAttached { get; set; }
        public string OtherAttachedInfor { get; set; }
    }
}
