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
    [RoutePrefix("api/KhieuNai")]
    public class KhieuNaiController : ApiController
    {
        QL_KHIEUNAIEntities _db = new QL_KHIEUNAIEntities();
        #region thêm mới khiếu nại
        /// <summary>
        /// Thêm khiếu nại mới.
        /// </summary>
        [HttpPost]
        [Route("Insert")]
        public async Task<IHttpActionResult> DT_KhieuNaiModify(DT_KHIEUNAI model)
        {
            //(KhieuNaiDTO datapost)
            try
            {
                await Task.Delay(100);
                #region gan dữ liệu
                //var model = new DT_KHIEUNAI()
                //{
                //    MS_KHIEUNAI = datapost.MS_KHIEUNAI,
                //    STTTINH = datapost.STTTINH,
                //    CHIEU = datapost.CHIEU,
                //    IDPTTL = datapost.IDPTTL,
                //    NGAYLAP = datapost.NGAYLAP,
                //    HETHAN = datapost.HETHAN,
                //    LOAIDV = datapost.LOAIDV,
                //    DICHVU = datapost.DICHVU,
                //    DICHVUDB = datapost.DICHVUDB,
                //    SOHIEUBG = datapost.SOHIEUBG,
                //    LYDOKN = datapost.LYDOKN,
                //    MUCDO = datapost.MUCDO,
                //    FILEDK = datapost.FILEDK,
                //    GHICHU = datapost.GHICHU,
                //    NGUOIGOI = datapost.NGUOIGOI,
                //    DCGOI = datapost.DIACHI,
                //    TINHGOI = datapost.TINHGOI,
                //    NUOCGOI = datapost.NUOCGOI,
                //    NGUOINHAN = datapost.NGUOINHAN,
                //    DCNHAN = datapost.DCNHAN,
                //    TINHNHAN = datapost.TINHNHAN,
                //    NUOCNHAN = datapost.NUOCNHAN,
                //    BCGOI = datapost.BCGOI,
                //    BCNHANKN = datapost.BCNHANKN,
                //    NGAYGOI = datapost.NGAYGOI,
                //    KHOILUONG = datapost.KHOILUONG,
                //    PLNOIDUNG = datapost.PLNOIDUNG,
                //    NOIDUNG = datapost.NOIDUNG,
                //    TRONGNUOC = datapost.TRONGNUOC,
                //    NGUOIKN = datapost.NGUOIKN,
                //    DIACHI = datapost.DIACHI,
                //    DTFAX = datapost.DTFAX,
                //    EMAIL = datapost.EMAIL,
                //    NGUOINHAP = datapost.NGUOINHAP,
                //    NGAYNHAP = datapost.NGAYNHAP,
                //    PHONGBAN = datapost.PHONGBAN,
                //    LANKN = datapost.LANKN,
                //    CHK = datapost.CHK,
                //    HUYKN = datapost.HUYKN,
                //    NGUOIHUY = datapost.NGUOIHUY,
                //    NGAYHUY = datapost.NGAYHUY,
                //    CUOC = datapost.CUOC,
                //    THOIGIAN = datapost.THOIGIAN,
                //    NGAYHH = datapost.NGAYHH,
                //    KNDACBIET = datapost.KNDACBIET,
                //    NGAYHOANTHANH = datapost.NGAYHOANTHANH,
                //    NGUYENNHAN = datapost.NGUYENNHAN,
                //    KHLON = datapost.KHLON,
                //    KETLUAN = datapost.KETLUAN,
                //    NGAYKETLUAN = datapost.NGAYKETLUAN,
                //    HTVCHUYEN = datapost.HTVCHUYEN,
                //    DATRUYEN = datapost.DATRUYEN,
                //    NGAYGIOIHAN = datapost.NGAYGIOIHAN,
                //    NGAYGIOIHANKH = datapost.NGAYGIOIHANKH,
                //    isKHL = datapost.isKHL,
                //    isDACBIET = datapost.isDACBIET
                //};
                #endregion
                #region check dữ liệu NULl
                //if (string.IsNullOrEmpty(model.MS_KHIEUNAI))
                //{
                //    return Ok(new ResponseInfo { Code = -999, ResponseMessage = "Mã số khiếu nại không được để trống" });
                //}
                if (model.LYDOKN == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Lý do khiếu nại không được để trống" });
                }
                if (model.DICHVU == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Loại dịch vụ khiếu nại không được để trống" });
                }
                if (model.NGAYGOI == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Ngày ký gửi khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.SOHIEUBG))
                {
                    return Ok(new ResponseCode { code = "error", message = "Số hiệu bưu gửi khiếu nại không được để trống" });
                }
                if (model.KHOILUONG == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Khối lượng khiếu nại không được để trống" });
                }
                if (model.CUOC == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Cước gửi khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.NGUOIGOI))
                {
                    return Ok(new ResponseCode { code = "error", message = "Họ tên người gửi khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.DCGOI))
                {
                    return Ok(new ResponseCode { code = "error", message = "Địa chỉ người gửi khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.NGUOINHAN))
                {
                    return Ok(new ResponseCode { code = "error", message = "Họ tên người nhận khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.DCNHAN))
                {
                    return Ok(new ResponseCode { code = "error", message = "Địa chỉ người nhận khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.BCNHANKN))
                {
                    return Ok(new ResponseCode { code = "error", message = "Bưu cụ thụ lý khiếu nại không được để trống" });
                }
                if (string.IsNullOrEmpty(model.NGUOIKN))
                {
                    return Ok(new ResponseCode { code = "error", message = "Họ tên người khiếu nại không được để trống" });
                }
                #endregion
                #region kiểm tra dữ liệu đầu vào
                //// kiểm tra lý do khiếu nại
                //temp = checkLyDoKhieuNai(model.LYDOKN);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],LYDOKN={0}, table = [DM_LYDOKN],ly do khieu nai khong thuoc danh muc ly do khieu nai", model.LYDOKN), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Lý do khiếu nại không thuộc danh mục lý do khiếu nại" });
                //}
                //// kiểm tra loại dịch vụ khiếu nại
                //temp = checkLoaiDichVu(model.LOAIDV);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],LOAIDV={0},table = [DM_DICHVU],loai dich vu khieu nai khong thuoc danh muc loai dich vu khieu nai", model.LOAIDV), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Loại dịch vụ khiếu nại không thuộc danh mục loại dịch vụ khiếu nại" });
                //}
                //// kiểm tra loại hàng khiếu nại
                //temp = checkLoaiHangHoa(model.PLNOIDUNG);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],PLNOIDUNG={0},table = [DM_LOAIHANG], loai noi dung khieu nai bi sai", model.PLNOIDUNG), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Loại nội dung khiếu nại không thuộc danh mục loại nội dung khiếu nại cho phép" });
                //}
                ////  kiểm tra loại dịch vụ
                //temp = checkLoaiDichVu(model.LOAIDV);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],LOAIDV={0},table = [DM_DICHVU],loai dich vu khieu nai bi sai", model.LOAIDV), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Loại dịch vụ khiếu nại không thuộc danh mục loại dịch vụ khiếu nại cho phép" });
                //}
                //// kiểm tra tỉnh thành phố
                //temp = checkTinhTP(int.Parse(model.STTTINH));
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],STTTINH={0},table = [DM_TINH_TP],id tinh khieu nai bi sai", model.STTTINH), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Tỉnh chọn khiếu nại không thuộc danh mục các tỉnh khiếu nại cho phép" });
                //}
                //// kiểm tra nước - quốc gia
                //temp = checkNuoc(model.NUOCGOI);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],NUOCGOI={0},table = [DM_NUOC], Nuoc gui khieu nai bi sai", model.NUOCGOI), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Nước gửi khiếu nại không thuộc danh mục các nước gửi khiếu nại cho phép" });
                //}
                //temp = checkNuoc(model.NUOCNHAN);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],NUOCNHAN={0},table = [DM_NUOC], Nuoc nhan khieu nai bi sai", model.NUOCNHAN), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Nước nhận khiếu nại không thuộc danh mục các nước nhận khiếu nại cho phép" });
                //}
                //// kiểu tra chiều khiếu nại
                //temp = checkChieu(model.CHIEU);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],CHIEU={0},table = [DM_CHIEU], ma chieu bi sai", model.CHIEU), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Chiều khiếu nại không thuộc danh mục chiều khiếu nại cho phép" });
                //}
                //// kiểm tra mức độ khiếu nại 1: Bình thường , 2: Khẩn
                //temp = checkMucDo(model.MUCDO);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],MUCDO={0},table = [Fix gia tri], ma chieu bi sai", model.MUCDO), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Mức độ khiếu nại không thuộc danh mục mức độ khiếu nại cho phép" });
                //}
                //// Kiểm tra phương thức trả lời
                //temp = checkPhuongThucTraloi(model.IDPTTL);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],IDPTTL={0},table = [DM_PTTRALOI], ma phuong thuc tra loi bi sai", model.IDPTTL), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Phương thức trả lời khiếu nại không thuộc danh mục phương thức trả lời khiếu nại cho phép" });
                //}
                //// kiểm tra phòng ban lấy theo tỉnh
                //temp = checkPhongBanByTinh(model.STTTINH, int.Parse(model.PHONGBAN));
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai], STTTINH={0},PHONGBAN={1} ,table = [DM_PHONGBAN], ", model.STTTINH, model.PHONGBAN), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Phòng ban không nằm trong danh sách phòng ban thuộc tỉnh được cho phép" });
                //}
                //// kiểm tra khách hàng lấy theo phòng ban
                //temp = checkKhachHangByPhongBan(int.Parse(model.PHONGBAN), model.KHLON);
                //if (temp < 1)
                //{
                //    LogHelper.LogInfo(string.Format("[ERR_KhieuNai],PHONGBAN={0},KHLON ={1},table = [DM_KHACHHANG], ma khach hang khong thuoc phong ban do", model.PHONGBAN, model.KHLON), "KhieuNai");
                //    return Ok(new ResponseInfo { Code = temp, ResponseMessage = "Khách hàng khiếu nại không thuộc danh sách khách hàng thuộc phòng ban cho phép" });
                //}
                #endregion

              

                model.MS_KHIEUNAI = createMSKHIEUNAI(model.CHIEU.ToString(), model.STTTINH);
                model.CHK = "0";
                model.DATRUYEN = "0";
                model.NGAYNHAP = DateTime.Now;
                model.NGAYLAP = model.NGAYLAP.HasValue ? model.NGAYLAP : DateTime.Now;
                model.THOIGIAN = int.Parse(getThoiGianByLoaiDv(model.DICHVU));
                string ngaylap = model.NGAYLAP.HasValue ? model.NGAYLAP.Value.ToString("yyyy/MM/dd") : DateTime.Now.ToString("yyyy/MM/dd");
                model.NGAYGIOIHAN = DateTime.Parse(NgayLamViec(ngaylap, model.THOIGIAN.ToString()));
                try
                {
                    _db.DT_KHIEUNAI.Add(model);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string err = string.Format("[ERR_KhieuNai] Thêm khiếu nại không thành công :  ex = {0},MS_KHIEUNAI={1}", ex.Message, model.MS_KHIEUNAI);
                    return Ok(new ResponseCode { code = "error", message = err });
                }
                try
                {
                    var dataDTKhieuNaiUser = new DT_KHIEUNAI_USER()
                    {
                        IDKHIEUNAI = model.MS_KHIEUNAI,
                        USERNAME = model.NGUOINHAP,
                        PHONGBAN = model.PHONGBAN,
                        CHK = "0",
                        USERTAO = model.NGUOINHAP,
                        DATRUYEN = "0"
                    };
                    _db.DT_KHIEUNAI_USER.Add(dataDTKhieuNaiUser);
                    _db.SaveChanges();
                    //var dt = KhieuNaiBLL.GetListPostSoLieuBySohieu(model.SOHIEUBG);
                    //var tempKhieunaichuyenthu = new DT_KHIEUNAI_CHUYENTHU();
                    //try
                    //{

                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (dt.Rows[0]["B_DICH_VU"].ToString().Trim() == "BP")
                    //        {
                    //            tempKhieunaichuyenthu.BCNHAN = "700910";
                    //        }
                    //        if (dt.Rows[0]["B_DICH_VU"].ToString().Trim() == "BK")
                    //        {
                    //            tempKhieunaichuyenthu.BCNHAN = "700920";
                    //        }
                    //        if (dt.Rows[0]["B_DICH_VU"].ToString().Trim() == "VEX")
                    //        {
                    //            tempKhieunaichuyenthu.BCNHAN = "710024";
                    //        }
                    //        tempKhieunaichuyenthu.IDKHIEUNAI = model.MS_KHIEUNAI;
                    //        tempKhieunaichuyenthu.BCGOC = dt.Rows[0]["BC_GOI"].ToString();
                    //        tempKhieunaichuyenthu.SOTUI = string.IsNullOrEmpty(dt.Rows[0]["TUI_THU"].ToString()) ? (int?)null : int.Parse(dt.Rows[0]["TUI_THU"].ToString());
                    //        tempKhieunaichuyenthu.SOCT = string.IsNullOrEmpty(dt.Rows[0]["CHUYEN_THU"].ToString()) ? (int?)null : int.Parse(dt.Rows[0]["TUI_THU"].ToString());
                    //        tempKhieunaichuyenthu.NGAYDONG = string.IsNullOrEmpty(dt.Rows[0]["NGAY_GOI"].ToString()) ? (DateTime?)null : DateTime.Parse(dt.Rows[0]["NGAY_GOI"].ToString());
                    //        tempKhieunaichuyenthu.PHUONGTHUC = string.IsNullOrEmpty(dt.Rows[0]["HTVCHUYEN"].ToString()) ? (int?)null : int.Parse(dt.Rows[0]["HTVCHUYEN"].ToString());
                    //        tempKhieunaichuyenthu.NGAYNHAP = DateTime.Now;
                    //        tempKhieunaichuyenthu.NGUOINHAP = model.UserName;
                    //        tempKhieunaichuyenthu.CHK = "0";
                    //        tempKhieunaichuyenthu.XOA = "1";
                    //        tempKhieunaichuyenthu.DONG = 0;
                    //        _db.DT_KHIEUNAI_CHUYENTHU.Add(tempKhieunaichuyenthu);
                    //        _db.SaveChanges();
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    string err = string.Format("[ERR_KhieuNai] loi insert [DT_KHIEUNAI_CHUYENTHU] dt.Rows.Count={0}, IDKHIEUNAI={1},BCNHAN={2},ex = {3}", dt.Rows.Count, model.MS_KHIEUNAI, tempKhieunaichuyenthu.BCNHAN, ex.Message);
                    //    LogHelper.LogInfo(err, "KhieuNai");
                    //}
                }
                catch (Exception ex)
                {
                    string err = string.Format("[ERR_KhieuNai] Thêm mới khiếu nại cho user không thành công :  MS_KHIEUNAI={0},USERNAME={1},PHONGBAN={2},ex={3}", model.MS_KHIEUNAI, model.NGUOINHAP, model.PHONGBAN, ex.Message);
                    return Ok(new ResponseCode { code = "error", message = err });
                }
                return Ok(new ResponseCode { code = "success", message = "Thêm mới khiếu nại thành công" });

            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] Lỗi trong quá trình xử lý :  ex = {0},MS_KHIEUNAI={1}", ex.Message, model.MS_KHIEUNAI);
                return Ok(new ResponseCode { code = "success", message = err });
            }
        }
        #endregion
        #region giải quyết khiếu nại
        /// <summary>
        /// Xử lý khiếu nại.
        /// </summary>
        [HttpPost]
        [Route("XuLyKhieuNai")]
        public async Task<IHttpActionResult> DT_KhieuNai_Xulykhieunai(XuLyKhieuNaiDTO model)
        {
            await Task.Delay(100);
            string sHoanThanh = "0";
            if (model.isHoanThanhKN == true) sHoanThanh = "3";
            if (string.IsNullOrEmpty(model.MaNguoiThuLy)) model.MaNguoiThuLy = "0";
            if (string.IsNullOrEmpty(model.MaNguoiThuLyTinh)) model.MaNguoiThuLyTinh = "0";
            model.NgayNhap = model.NgayNhap == null ? DateTime.Now : model.NgayNhap;
            model.NgayHT = model.NgayHT == null ? DateTime.Now : model.NgayHT;
            int checkCHK = 0;
            #region checkNull
            if (string.IsNullOrEmpty(model.MSKhieuNai))
            {
                return Ok(new ResponseCode { code = "error", message = "Mã số khiếu nại không được bỏ trống" });
            }
            if (string.IsNullOrEmpty(model.NguoiNhap))
            {
                return Ok(new ResponseCode { code = "error", message = "Người nhập không được bỏ trống" });
            }
            if (model.IdKQ == 1 && model.NgayPhat == null)
            {
                return Ok(new ResponseCode { code = "error", message = "Ngày và giờ phát không được bỏ trống" });
            }
            if (model.isHoanThanhKN == true && model.IdKQ < 1)
            {
                return Ok(new ResponseCode { code = "error", message = "Bạn chưa chọn kết quả khiếu nại" });
            }
            if (model.isHoanThanhKN == true && model.IdKL < 1)
            {
                return Ok(new ResponseCode { code = "error", message = "Hồ sơ chưa có kết luận. Không thể hoàn thành" });
            }
            var checkMSKN = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
            if (checkMSKN == null)
            {
                return Ok(new ResponseCode { code = "error", message = "Mã khiếu nại này không tồn tại" });
            }
            #endregion
            try
            {
                if (model.isKNDacBiet == true)
                {
                    var record = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    record.isDACBIET = 1;
                    _db.SaveChanges();
                };

                string nguoithuly = (model.MaNguoiThuLyTinh != "0") ? model.MaNguoiThuLyTinh : model.MaNguoiThuLy;
                var tempTienTrinh = new DT_KHIEUNAI_TIENTRINH()
                {
                    IDKHIEUNAI = model.MSKhieuNai,
                    NOIDUNG = model.ThongTinTraoDoi,
                    NGAYCHUYEN = model.NgayNhap,
                    NGAYPHAT = model.NgayPhat,
                    KETQUA = model.IdKQ,
                    NOIDUNGTA = model.ChuyenThuTA,
                    NGUOINHAP = model.NguoiNhap,
                    NGAYNHAP = model.NgayNhap,
                    CHK = sHoanThanh,
                    FILEDK = model.PathFile,
                    XULY = "1",
                    NOIDUNGBC08 = model.ChiTietKetQua,
                    NGUOIXLYTIEP = nguoithuly,
                    DATRUYEN = "0"
                };
                _db.DT_KHIEUNAI_TIENTRINH.Add(tempTienTrinh);
                _db.SaveChanges();

                #region insert tiến trình
                //if (model.MaNguoiThuLyTinh != "0" && !string.IsNullOrEmpty(model.MaNguoiThuLyTinh))
                //{
                //    var tempTienTrinh = new DT_KHIEUNAI_TIENTRINH()
                //    {
                //        IDKHIEUNAI = model.MSKhieuNai,
                //        NOIDUNG = model.ThongTinTraoDoi,
                //        NGAYCHUYEN = model.NgayNhap,
                //        NGAYPHAT = model.NgayPhat,
                //        KETQUA = model.IdKQ,
                //        NOIDUNGTA = model.ChuyenThuTA,
                //        NGUOINHAP = model.NguoiNhap,
                //        NGAYNHAP = model.NgayNhap,
                //        CHK = sHoanThanh,
                //        FILEDK = model.PathFile,
                //        XULY = "1",
                //        NOIDUNGBC08 = model.ChiTietKetQua,
                //        NGUOIXLYTIEP = model.MaNguoiThuLyTinh,
                //        DATRUYEN = "0"

                //    };
                //    _db.DT_KHIEUNAI_TIENTRINH.Add(tempTienTrinh);
                //    _db.SaveChanges();
                //}

                //if (model.MaNguoiThuLy != "0" && !string.IsNullOrEmpty(model.MaNguoiThuLy))
                //{
                //    var tempTienTrinh = new DT_KHIEUNAI_TIENTRINH()
                //    {
                //        IDKHIEUNAI = model.MSKhieuNai,
                //        NOIDUNG = model.ThongTinTraoDoi,
                //        NGAYCHUYEN = model.NgayNhap,
                //        NGAYPHAT = model.NgayPhat,
                //        KETQUA = model.IdKQ,
                //        NOIDUNGTA = model.ChuyenThuTA,
                //        NGUOINHAP = model.NguoiNhap,
                //        NGAYNHAP = model.NgayNhap,
                //        CHK = sHoanThanh,
                //        FILEDK = model.PathFile,
                //        XULY = "1",
                //        NOIDUNGBC08 = model.ChiTietKetQua,
                //        NGUOIXLYTIEP = model.MaNguoiThuLy,
                //        DATRUYEN = "0"

                //    };
                //    _db.DT_KHIEUNAI_TIENTRINH.Add(tempTienTrinh);
                //    _db.SaveChanges();
                //}
                //if ((model.MaNguoiThuLyTinh == "0" || string.IsNullOrEmpty(model.MaNguoiThuLyTinh)) && (model.MaNguoiThuLy == "0" || string.IsNullOrEmpty(model.MaNguoiThuLy)))
                //{
                //    var tempTienTrinh = new DT_KHIEUNAI_TIENTRINH()
                //    {
                //        IDKHIEUNAI = model.MSKhieuNai,
                //        NOIDUNG = model.ThongTinTraoDoi,
                //        NGAYCHUYEN = model.NgayNhap,
                //        NGAYPHAT = model.NgayPhat,
                //        KETQUA = model.IdKQ,
                //        NOIDUNGTA = model.ChuyenThuTA,
                //        NGUOINHAP = model.NguoiNhap,
                //        NGAYNHAP = model.NgayNhap,
                //        CHK = sHoanThanh,
                //        FILEDK = model.PathFile,
                //        XULY = "1",
                //        NOIDUNGBC08 = model.ChiTietKetQua,
                //        NGUOIXLYTIEP = model.MaNguoiThuLy,
                //        DATRUYEN = "0"

                //    };
                //    _db.DT_KHIEUNAI_TIENTRINH.Add(tempTienTrinh);
                //    _db.SaveChanges();
                //}
                #endregion
                if (model.SoLanKN == 2)//KHIEU NAI LAN 23456...
                {
                    if (model.MaNguoiThuLy == "0")
                    {
                        return Ok(new ResponseCode { code = "error", message = "Bạn chưa chọn chuyển khiếu nại" });
                    }
                    // giai quyet cho TH nguoi KN tiep ko la nguoi lap KN

                    var tempKieuNai = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    tempKieuNai.LANKN = (short)(tempKieuNai.LANKN + 1);
                    tempKieuNai.CHK = "0";
                    _db.SaveChanges();

                    var tempKieuNaiUser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (tempKieuNaiUser != null)
                    {
                        tempKieuNaiUser.CHK = "0";
                        _db.SaveChanges();
                    }
                }
                if (model.MaNguoiThuLy != "0" || model.MaNguoiThuLyTinh != "0")
                {
                    string tempNguoithulyuser = model.MaNguoiThuLy != "0" ? model.MaNguoiThuLy : model.MaNguoiThuLyTinh;
                    var tempKNUser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == tempNguoithulyuser && m.USERTAO == model.NguoiNhap).FirstOrDefault();
                    if (tempKNUser != null)
                    {
                        tempKNUser.CHK = "0";
                        _db.SaveChanges();
                    }
                    else
                    {
                        var tempAddKNUSER = new DT_KHIEUNAI_USER()
                        {
                            IDKHIEUNAI = model.MSKhieuNai,
                            USERNAME = tempNguoithulyuser,
                            PHONGBAN = model.MaNguoiThuLyTinh != "0" ? "0" : CheckPhongBanByUserName(model.MaNguoiThuLy),
                            CHK = "0",
                            USERTAO = model.NguoiNhap,
                            DATRUYEN = "0"
                        };
                        _db.DT_KHIEUNAI_USER.Add(tempAddKNUSER);
                        _db.SaveChanges();
                    }
                    var tempUpdateKNUSERByMaNguoiThuLyTinh = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == tempNguoithulyuser).FirstOrDefault();
                    tempUpdateKNUSERByMaNguoiThuLyTinh.CHK = "0";
                    _db.SaveChanges();

                    //// CAP NHAT DA XEM
                    var checkUpdateKNDX = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(tempNguoithulyuser, model.MSKhieuNai, "0");
                    if (checkUpdateKNDX <= 0)
                    {
                        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByMaNguoiThuLyTinh(tempNguoithulyuser, model.MSKhieuNai);
                    }
                    var checkUpdateKNDXByNguoiNhap = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(model.NguoiNhap, model.MSKhieuNai, "1");
                    if (checkUpdateKNDXByNguoiNhap <= 0)
                    {
                        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByNguoiNhap(model.NguoiNhap, model.MSKhieuNai);
                    }
                }
                #region xử lý khiếu nại user
                //if (model.MaNguoiThuLyTinh != "0")
                //{
                //    var tempKNUser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.MaNguoiThuLyTinh && m.USERTAO == model.NguoiNhap).FirstOrDefault();
                //    if (tempKNUser != null)
                //    {
                //        try
                //        {
                //            tempKNUser.CHK = "0";
                //            _db.SaveChanges();
                //        }
                //        catch (Exception ex)
                //        {
                //            var tempAddKNUSER = new DT_KHIEUNAI_USER()
                //            {
                //                IDKHIEUNAI = model.MSKhieuNai,
                //                USERNAME = model.MaNguoiThuLyTinh,
                //                PHONGBAN = "0",
                //                CHK = "0",
                //                USERTAO = model.NguoiNhap,
                //                DATRUYEN = "0"
                //            };
                //            _db.DT_KHIEUNAI_USER.Add(tempAddKNUSER);
                //            _db.SaveChanges();
                //        }
                //    }
                //    var tempUpdateKNUSERByMaNguoiThuLyTinh = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.MaNguoiThuLyTinh).FirstOrDefault();
                //    tempUpdateKNUSERByMaNguoiThuLyTinh.CHK = "0";
                //    _db.SaveChanges();

                //    //// CAP NHAT DA XEM
                //    var checkUpdateKNDX = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(model.MaNguoiThuLyTinh, model.MSKhieuNai, "0");
                //    if (checkUpdateKNDX == false)
                //    {
                //        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByMaNguoiThuLyTinh(model.MaNguoiThuLyTinh, model.MSKhieuNai);
                //    }
                //    var checkUpdateKNDXByNguoiNhap = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(model.NguoiNhap, model.MSKhieuNai, "1");
                //    if (checkUpdateKNDXByNguoiNhap == false)
                //    {
                //        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByNguoiNhap(model.NguoiNhap, model.MSKhieuNai);
                //    }
                //}
                //if (model.MaNguoiThuLy != "0")
                //{
                //    var tempKNUser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.MaNguoiThuLy && m.USERTAO == model.NguoiNhap).FirstOrDefault();
                //    if (tempKNUser != null)
                //    {
                //        try
                //        {
                //            tempKNUser.CHK = "0";
                //            _db.SaveChanges();
                //        }
                //        catch (Exception ex)
                //        {
                //            var tempAddKNUSER = new DT_KHIEUNAI_USER()
                //            {
                //                IDKHIEUNAI = model.MSKhieuNai,
                //                USERNAME = model.MaNguoiThuLy,
                //                PHONGBAN = CheckPhongBanByUserName(model.MaNguoiThuLy),
                //                CHK = "0",
                //                USERTAO = model.NguoiNhap,
                //                DATRUYEN = "0"
                //            };
                //            _db.DT_KHIEUNAI_USER.Add(tempAddKNUSER);
                //            _db.SaveChanges();
                //        }
                //    }
                //    var tempUpdateKNUSERByUsername = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.NguoiNhap).FirstOrDefault();
                //    tempUpdateKNUSERByUsername.CHK = "0";
                //    _db.SaveChanges();

                //    //// CAP NHAT DA XEM
                //    var checkUpdateKNDXByMaNguoiXuLy = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(model.MaNguoiThuLy, model.MSKhieuNai, "0");
                //    if (checkUpdateKNDXByMaNguoiXuLy == false)
                //    {
                //        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByMaNguoiThuLyTinh(model.MaNguoiThuLy, model.MSKhieuNai);
                //    }
                //    var checkUpdateKNDXByNguoiNhap = KhieuNaiBLL.DT_KhieuNai_DaXem_Update(model.NguoiNhap, model.MSKhieuNai, "1");
                //    if (checkUpdateKNDXByNguoiNhap == false)
                //    {
                //        KhieuNaiBLL.DT_KhieuNai_DaXem_InsertByNguoiNhap(model.NguoiNhap, model.MSKhieuNai);
                //    }

                //}
                #endregion

                if (sHoanThanh == "3" && model.IdKL != 0 && model.IdKQ != 0)
                {
                    var temp = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    temp.CHK = "3";
                    temp.KETLUAN = model.IdKL;
                    temp.NGUYENNHAN = model.IdKQ;
                    temp.NGAYHOANTHANH = model.NgayHT;
                    _db.SaveChanges();

                    var tempknuser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (tempknuser != null)
                    {
                        tempknuser.CHK = "3";
                        _db.SaveChanges();
                    }
                }
                else
                {
                    if (model.IdKQ != 0)
                    {
                        var temp = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                        temp.CHK = "2";
                        checkCHK = 2;
                        temp.KETLUAN = model.IdKL;
                        _db.SaveChanges();

                        var tempknuser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.NguoiNhap).FirstOrDefault();
                        if (tempknuser != null)
                        {
                            tempknuser.CHK = "1";
                            _db.SaveChanges();
                        }

                    }
                    if (model.IdKQ == 0 && (model.MaNguoiThuLy != "0" || model.MaNguoiThuLyTinh != "0"))
                    {
                        var temp = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                        temp.CHK = "1";
                        checkCHK = 1;
                        temp.KETLUAN = model.IdKL;
                        _db.SaveChanges();

                        var tempknuser = _db.DT_KHIEUNAI_USER.Where(m => m.IDKHIEUNAI == model.MSKhieuNai && m.USERNAME == model.NguoiNhap).FirstOrDefault();
                        if (tempknuser != null)
                        {
                            tempknuser.CHK = "1";
                            _db.SaveChanges();
                        }
                    }
                }

                if (model.isHuyKN == true)
                {
                    var temp = _db.DT_KHIEUNAI.Where(m => m.MS_KHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.CHK = "4";
                        temp.NGUOIHUY = model.NguoiNhap;
                        temp.NGAYNHAP = model.NgayNhap;
                        _db.SaveChanges();
                    }
                }


                //UPDATE CAC TRUONG DA XEM DE CANH BAO

                KhieuNaiBLL.DT_KhieuNai_DaXem_UpdateAllNotEqual(model.NguoiNhap, model.MSKhieuNai, "0");

                // ghi vao bang boi thuong de xu ly boi thuong
                if (model.isXLBoiThuongKN == true && model.isHuyKN == false)
                {
                    var temp = _db.DT_KHIEUNAI_BOITHUONG.Where(m => m.IDKHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (temp != null)
                    {
                        try
                        {
                            temp.TRANGTHAI = "1";
                            temp.NGUOINHAP = model.NguoiNhap;
                            temp.NGAYNHAP = model.NgayNhap;
                            temp.DA_CHI_BT = "1";
                            _db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            var temp1 = new DT_KHIEUNAI_BOITHUONG()
                            {
                                IDKHIEUNAI = model.MSKhieuNai,
                                TRANGTHAI = "1",
                                NGUOINHAP = model.NguoiNhap,
                                NGAYNHAP = model.NgayNhap,
                                DA_CHI_BT = "0",
                                CHK = "0"
                            };
                            _db.DT_KHIEUNAI_BOITHUONG.Add(temp1);
                            _db.SaveChanges();
                        }

                    }
                }
                // CAP NHAT DA CHI TIEN BOI THUONG
                if (model.isXNChiBT == true)
                {
                    var temp = _db.DT_KHIEUNAI_BOITHUONG.Where(m => m.IDKHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.DA_CHI_BT = "1";
                        _db.SaveChanges();
                    }
                }
                if (model.isXNKhongChiBT == true)
                {
                    var temp = _db.DT_KHIEUNAI_BOITHUONG.Where(m => m.IDKHIEUNAI == model.MSKhieuNai).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.DA_CHI_BT = "0";
                        _db.SaveChanges();
                    }
                }

                // thông báo
                string mes = string.Empty;
                if (sHoanThanh == "3")
                {
                    mes = "Khiếu nại đã được giải quyết";
                }
                else
                {
                    if (checkCHK == 1)
                    {
                        mes = "Khiếu nại đang giải quyết vì chưa xác định được kết quả";
                    }
                    if (checkCHK == 2)
                    {
                        mes = "Khiếu nại chưa giải quyết và đã có kết quả rồi.";
                    }
                    if(checkCHK == 0)
                    {
                        mes = "Mã người thụ lý or mã người thụ lý tỉnh bằng null. Cần check lại logic";
                    }
                }
                return Ok(new ResponseCode { code = "success", message = mes });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] MSKhieuNai={0},ex ={1},", model.MSKhieuNai, ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Thêm mới khiếu nại chuyến thư.
        /// </summary>
        [HttpPost]
        [Route("InsertKhieuNaiChuyenThu")]
        public async Task<IHttpActionResult> DT_KHIEUNAI_CHUYENTHUInsert(DT_KHIEUNAI_CHUYENTHU model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.IDKHIEUNAI))
                {
                    return Ok(new ResponseCode { code = "error", message = "Mã số khiếu nại không được để trông" });
                }
                model.CHK = "0";
                model.NGAYNHAP = DateTime.Now;
                model.XOA = "0";
                model.DATRUYEN = "0";
                _db.DT_KHIEUNAI_CHUYENTHU.Add(model);
                _db.SaveChanges();
                return Ok(new ResponseCode { code = "success", message = "Thêm chuyến thư thành công" });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi insert thông tin trong bảng [DT_KHIEUNAI_CHUYENTHU] :   ex = {0},tinh={1}", ex.Message);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        #endregion
        #region Hàm check
        private int checkLyDoKhieuNai(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_LYDOKN.Where(m => m.ID == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkLoaiDichVu(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_DICHVU.Where(m => m.ID == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkLoaiHangHoa(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_LOAIHANG.Where(m => m.IDLH == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkTinhTP(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_TINH_TP.Where(m => m.ID_Tinh_TP == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkNuoc(string ma)
        {
            if (ma == null || ma == "")
            {
                return -1;
            }
            else
            {
                var data = _db.DM_NUOC.Where(m => m.MA_NUOC == ma).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkChieu(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_CHIEU.Where(m => m.IDC == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkMucDo(int? Id)
        {
            if (Id == 1 || Id == 2)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }
        private int checkPhuongThucTraloi(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_PTTRALOI.Where(m => m.IDPT == Id).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkPhongBanByTinh(string matinh, int maphongban)
        {
            if (matinh == null)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_PHONGBAN.Where(m => m.BUUDIEN == matinh && m.MSPB == maphongban).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        private int checkKhachHangByPhongBan(int maphongban, string makh)
        {
            if (makh == null || makh == "" || maphongban == null || maphongban == 0)
            {
                return -1;
            }
            else
            {
                var data = _db.DM_KHACHHANG.Where(m => m.DONVI == maphongban && m.MAKH == makh).FirstOrDefault();
                if (data == null)
                {
                    return -1;
                }
                return 1;
            }
        }
        public string CheckPhongBanByUserName(string username)
        {
            var data = _db.SYS_USER.Where(m => m.USERNAME == username).FirstOrDefault();
            if (data != null)
            {
                return data.PHONGBAN;
            }
            return "0";
        }
        #endregion
        private string createMSKHIEUNAI(string sChieu, string tinh)
        {
            string sNam = DateTime.Now.Year.ToString().Substring(2, 2);
            string sSH = "", sStt = "", sN = "", sSTTTinh = "";
            int iStt = 0;

            var dt = KhieuNaiBLL.GetListSYSCONBySTTTinh(tinh);
            if (dt.Rows.Count > 0)
            {
                iStt = int.Parse(dt.Rows[0]["STT"].ToString());
                sN = dt.Rows[0]["NAM"].ToString();
                sSTTTinh = dt.Rows[0]["STTTINH"].ToString();
            }
            if (sN != sNam)
            {
                KhieuNaiBLL.SYSCONUpdateName(sNam, tinh);
                iStt = 1;
            }
            else
            {
                KhieuNaiBLL.SYSCONUpdateSTT(tinh);
            }

            sStt = iStt.ToString();
            for (int i = 0; i < 6 - iStt.ToString().Length; i++)
            {
                sStt = "0" + sStt;
            }

            //  sSH = sSTTTinh + sChieu + sNam + sStt;

            sSH = sSTTTinh + sNam + sStt;
            return sSH;
        }
        private string getThoiGianByLoaiDv(int? sID)
        {
            var dt = KhieuNaiBLL.GetThoiGianByLoaiDV(sID);
            var s = "0";
            if (dt.Rows.Count > 0)
            {
                s = dt.Rows[0]["THOIGIAN"].ToString();
            }
            return s;
        }
        private string NgayLamViec(string sDau, string sNgayDV)
        {
            DateTime First = new DateTime(); // ngày bắt đầu làm việc
            DateTime Last = new DateTime(); // ngày kết thúc làm việc
            int sNgayLe = 0;

            sDau = sDau.Trim();

            First = DateTime.Parse(sDau);
            Last = DateTime.Parse(sDau).AddDays(int.Parse(sNgayDV));

            if (sDau != "")
            {
                var dt = KhieuNaiBLL.CheckNgayLamViec(First.ToString("MM/dd/yyyy"), Last.ToString("MM/dd/yyyy"));

                if (dt.Rows.Count > 0)
                {
                    sNgayLe = int.Parse(dt.Rows[0]["NGAYLE"].ToString());
                }


                double sTongNgay = 0;// (Last - First).Days;

                DateTime FirstSunday = First.AddDays(7 - (int)First.DayOfWeek); // ngày chủ nhật đầu tiên
                DateTime LastSunday = Last.AddDays(-(int)Last.DayOfWeek); // ngày chủ nhật cuối cùng
                int SundayCount = LastSunday.Subtract(FirstSunday).Days / 7 + 1; // tổng số ngày chủ nhật

                DateTime FirstSaturday = First.AddDays(6 - (int)First.DayOfWeek); // ngày thu 7 đầu tiên
                DateTime LastSaturday = Last.AddDays(-(int)Last.DayOfWeek); // ngày thu 7 cuối cùng
                int SaturdayCount = LastSaturday.Subtract(FirstSaturday).Days / 7 + 1; // tổng số ngày thu 7

                sTongNgay = SaturdayCount + SundayCount + sNgayLe + int.Parse(sNgayDV);

                Last = DateTime.Parse(sDau).AddDays(sTongNgay);
                if (sTongNgay <= 0) sTongNgay = 0;

                return Last.ToString();
            }
            else
            {
                return "";
            }

        }
       


    }
}
