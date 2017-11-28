using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AddMailTrip
{
    public class SmartphoneDTO
    {
        public long Id { get; set; }
        public int Ma_Bc_Khai_Thac { get; set; }
        public int Ma_Bc { get; set; }
        public int So_Chuyen_Thu { get; set; }
        public int Ngay_Dong { get; set; }
        public DateTime Thoi_Gian_Dong { get; set; }
        public int Tui_So { get; set; }
        public int Trang_Thai { get; set; }
    }
}
