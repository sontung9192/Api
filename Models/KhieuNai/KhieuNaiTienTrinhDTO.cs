using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.KhieuNai
{
    public class KhieuNaiTienTrinhDTO
    {
        public decimal IDCTKN { get; set; }
        public string IDKHIEUNAI { get; set; }
        public string NOIDUNG { get; set; }
        public string NOIDUNGTA { get; set; }
        public string NOIDUNGBC08 { get; set; }
        public Nullable<System.DateTime> NGAYCHUYEN { get; set; }
        public Nullable<System.DateTime> NGAYPHAT { get; set; }
        public string FILEDK { get; set; }
        public Nullable<int> KETQUA { get; set; }
        public string NGUOINHAP { get; set; }
        public Nullable<System.DateTime> NGAYNHAP { get; set; }
        public string XULY { get; set; }
        public string CHK { get; set; }
        public string NGUOIXLYTIEP { get; set; }
        public string DATRUYEN { get; set; }
    }
}
