using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.KhieuNai
{
    public class XuLyKhieuNaiDTO
    {
        public string MSKhieuNai { get; set; }
        public int IdKQ { get; set; }
        public int IdKL { get; set; }
        public string ChiTietKetQua { get; set; }
        public string ChuyenThuTA { get; set; }
        public string ThongTinTraoDoi { get; set; }
        public DateTime? NgayNhap { get; set; }
        public string NguoiNhap { get; set; }
        public string PathFile { get; set; }
        public DateTime? NgayHT { get; set; }
        public DateTime? NgayPhat { get; set; }
        public string MaNguoiThuLy { get; set; }
        public int IdPhongBan { get; set; }
        public int IdTinh { get; set; }
        public string MaNguoiThuLyTinh { get; set; }
        public bool isHuyKN { get; set; }
        public bool isHoanThanhKN { get; set; }
        public bool isXLBoiThuongKN { get; set; }
        public bool isKNDacBiet { get; set; }
        public int SoLanKN { get; set; }
        public bool isXNChiBT { get; set; }
        public bool isXNKhongChiBT { get; set; }
    }
    public class ThoiGianDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CHK { get; set; }
        public string NguoiNhap { get; set; }
        public string UserName { get; set; }
    }
    public class GetKhieuNaiTienTrinhDTO
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string IDKhieuNai { get; set; }
        public string UserName { get; set; }
    }
    public class GetSysUserDTO
    {
        public string UserName { get; set; }
        public string Password {get; set; }
    }
    public class GetKhieuNaiUserDTO
    {
        public string IdKhieuNai { get; set; }
        public string UserName { get; set; }
    }
}
