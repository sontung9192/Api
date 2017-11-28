using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Models.KhieuNai;
using ApiProject.Models;
using Utils;
using DataAccess;
using System.Data.SqlClient;

namespace ApiProject.Controllers
{
    [Authorize]
    [RoutePrefix("api/DanhMuc")]
    public class DanhMucController : ApiController
    {
        QL_KHIEUNAIEntities _db = new QL_KHIEUNAIEntities();
        #region get thông tin form khiếu nại
        /// <summary>
        /// Danh mục lý do khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSLyDoKhieuNai")]
        public async Task<IHttpActionResult> DSLyDoKhieuNai()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_LYDOKN.Select(m => new { m.ID, m.LYDO, m.CHK }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách lý do khiếu nại", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_LYDOKN] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục dịch vụ khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSDichVu")]
        public async Task<IHttpActionResult> DSDichVu()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_DICHVU.Select(m => new { m.ID, m.TENTAT, m.TENDICHVU, m.LOAIDV, m.THOIGIAN, m.CHK, m.GHICHU }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách dịch vụ khiếu nại", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_DICHVU] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục loại mặt hàng khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSLoaiHangHoa")]
        public async Task<IHttpActionResult> DSLoaiHoangHoa()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_LOAIHANG.Select(m => new { m.IDLH, m.TENLH, m.CHK }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các loại hàng hóa", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_LOAIHANG] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục tỉnh thành phố khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSTinhThanhPho")]
        public async Task<IHttpActionResult> DSTinhThanhPho()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_TINH_TP.Select(m => new { m.ID_Tinh_TP, m.STT_Tinh_TP, m.Ten_Tinh_TP, m.TEN_BD, m.IP_Tinh_TP, m.Ma_vung_BK, m.Ma_Vung_EMS, m.MA_TT_KT, m.NHOM_TINH }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các tỉnh thành phố", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_TINH_TP] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục nước khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSNuoc")]
        public async Task<IHttpActionResult> DSNuoc()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_NUOC.Select(m => new { m.MA_NUOC, m.TEN_NUOC, m.MA_VUNG, m.TEN_VUNG, m.CACH_TINH_CUOC, m.DON_VI_TIEN_TE, m.TY_LE_CUOC }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các nước", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_NUOC] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục chiều khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSChieu")]
        public async Task<IHttpActionResult> DSChieu()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_CHIEU.Select(m => new { m.IDC, m.TENCHIEU }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các chiều", data = data });

            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_CHIEU] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục phương thức trả lời khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSPhuongThucTraLoi")]
        public async Task<IHttpActionResult> DSPhuongThucTraLoi()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_PTTRALOI.Select(m => new { m.IDPT, m.TENPT }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các phương thức trả lời", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_PTTRALOI] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục phòng ban khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSPhongBan")]
        public async Task<IHttpActionResult> DSPhongBanByTinh(string tinh)
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_PHONGBAN.Where(m => m.BUUDIEN == tinh).OrderBy(m => m.TENPB).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các phòng ban thuộc tỉnh " + tinh, data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_PHONGBAN] :   ex = {0},tinh={1}", ex.Message, tinh);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh sách khách hàng.
        /// </summary>
        [HttpGet]
        [Route("DSKhachHang")]
        public async Task<IHttpActionResult> DSKhachHangByPhongBan(int phongban)
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_KHACHHANG.Where(m => m.DONVI == phongban).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các khách hàng thuộc phòng ban " + phongban, data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_KHACHHANG] :   ex = {0},tinh={1}", ex.Message, phongban);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục phương thức.
        /// </summary>
        [HttpGet]
        [Route("DSPhuongThuc")]
        public async Task<IHttpActionResult> DSPhuongThuc()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_PHUONGTHUC.Select(m => new { m.IDPT, m.TENPT }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các phương thức vận chuyển ", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_PHUONGTHUC] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh sách người thụ lý.
        /// </summary>
        [HttpGet]
        [Route("DSNguoiThuLy")]
        public async Task<IHttpActionResult> DSNguoiThuLy(string username, string maphong)
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.SYS_USER.Where(m => m.USERNAME != username && m.PHONGBAN == maphong).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các người thụ lý ", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_PHUONGTHUC] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh sách tỉnh nhận khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("DSTinhNhanKhieuNai")]
        public async Task<IHttpActionResult> DSTinhNhanKhieuNai(int tinh)
        {
            try
            {
                await Task.Delay(1000);
                var data = (from t in _db.DM_TINH_TP
                            join u in _db.SYS_USER on t.STT_Tinh_TP equals u.STTTINH
                            where t.STT_Tinh_TP != tinh
                            select new
                            {
                                t.Ten_Tinh_TP,
                                t.STT_Tinh_TP
                            }).Distinct();
                return Ok(new ResponseCode { code = "success", message = "danh sách các tỉnh nhận khiếu nại ", data = data.OrderBy(m => m.Ten_Tinh_TP) });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_TINH_TP & SYS_USER] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh sách người thụ lý tỉnh.
        /// </summary>
        [HttpGet]
        [Route("DSNguoiThuLyTinh")]
        public async Task<IHttpActionResult> DSNguoiThuLyTinh(string tinh, string idkhieunai)
        {
            try
            {
                await Task.Delay(1000);
                if (string.IsNullOrEmpty(tinh))
                {
                    return Ok(new ResponseCode { code = "error", message = "Mã tỉnh không được bỏ trống" });
                }
                if (string.IsNullOrEmpty(idkhieunai))
                {
                    return Ok(new ResponseCode { code = "error", message = "Mã khiếu nại không được bỏ trống" });
                }
                var data = KhieuNaiBLL.GetListNguoiThuLyTinh(tinh, idkhieunai);
                return Ok(new ResponseCode { code = "success", message = "danh người thụ lý tỉnh ", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DT_KHIEUNAI_USER & SYS_USER] :   ex = {0},matinh={1},idkhieunai={2}", ex.Message, tinh, idkhieunai);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục kết quả.
        /// </summary>
        [HttpGet]
        [Route("DSKetQua")]
        public async Task<IHttpActionResult> DSKetQua()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_KETQUA.Select(m => new { m.IDKQ, m.TENKQ, m.CHK }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách kết quả ", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_KETQUA] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh mục kết luận.
        /// </summary>
        [HttpGet]
        [Route("DSKetLuan")]
        public async Task<IHttpActionResult> DSKetLuan()
        {
            try
            {
                await Task.Delay(1000);
                var data = _db.DM_KETLUAN.Select(m => new { m.IDKL, m.TENKL, m.CHK }).ToList();
                return Ok(new ResponseCode { code = "success", message = "danh sách các kết luận ", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DM_KETLUAN] :   ex = {0}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        #endregion
    }
}
