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
    [RoutePrefix("api/ThongTin")]
    public class ThongTinController : ApiController
    {
        QL_KHIEUNAIEntities _db = new QL_KHIEUNAIEntities();
        #region get thông tin khiếu nại
        /// <summary>
        /// Danh sách khiếu nại.
        /// </summary>
        [HttpGet]
        [Route("GetListKhieuNai")]
        public async Task<IHttpActionResult> GetListKhieuNai(DateTime? fromdate, DateTime? todate, string chk, string nguoinhap, string username)
        {
            try
            {
                await Task.Delay(1000);

                if (fromdate == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Thời gian từ ngày không được bỏ trống" });
                }
                if (todate == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Thời gian đến ngày không được bỏ trống" });
                }

                if (string.IsNullOrEmpty(nguoinhap))
                {
                    return Ok(new ResponseCode { code = "error", message = "Người nhập nhập không được bỏ trống. Vui lòng nhập lại !" });
                }
                ThoiGianDTO model = new ThoiGianDTO()
                {
                    FromDate = fromdate,
                    ToDate = todate,
                    CHK = chk,
                    NguoiNhap = nguoinhap,
                    UserName = username

                };
                var data = KhieuNaiBLL.GetListKhieuNai(model);
                return Ok(new ResponseCode { code = "success", message = "Lấy danh sách khiếu nại", data = data });

            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] lỗi get thông tin trong bảng [DT_KHIEUNAI] :   ex = {0},FromDate={1},ToDate={2},CHK={3},NguoiNhap={4},username={5}", ex.Message, fromdate, todate, chk, nguoinhap, username);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Danh sách các tiến trình.
        /// </summary>
        [HttpGet]
        [Route("GetListKhieuNaiTienTrinh")]
        public async Task<IHttpActionResult> GetListKhieuNaiTienTrinh(DateTime? fromdate, DateTime? todate, string idkhieunai, string username)
        {
            var model = new GetKhieuNaiTienTrinhDTO()
            {
                FromDate = fromdate,
                ToDate = todate,
                IDKhieuNai = idkhieunai,
                UserName = username
            };
            try
            {
                await Task.Delay(100);
                if (model.FromDate == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Thời gian từ ngày không được bỏ trống" });
                }
                if (model.ToDate == null)
                {
                    return Ok(new ResponseCode { code = "error", message = "Thời gian đến ngày không được bỏ trống" });
                }
                if (string.IsNullOrEmpty(model.IDKhieuNai))
                {
                    return Ok(new ResponseCode { code = "error", message = "Mã khiếu nại không được bỏ trống" });
                }
                var data = KhieuNaiBLL.GetListKhieuNaiTienTrinh(model);
                return Ok(new ResponseCode { code = "success", message = "Lấy danh sách khiếu nại tiến trình", data = data });
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] ex = {0},FromDate={1},ToDate={2},IDKhieuNai={3},UserName={4}", ex.Message, model.FromDate, model.ToDate, model.IDKhieuNai, model.UserName);
                return Ok(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Lấy thông tin user khi đăng nhập.
        /// </summary>
        [HttpGet]
        [Route("GetListUser")]
        public async Task<IHttpActionResult> GetListSysUser(string username, string password)
        {
            var model = new GetSysUserDTO()
            {
                UserName = username,
                Password = password
            };
            try
            {
                await Task.Delay(1000);
                var data = _db.SYS_USER.Where(m => m.USERNAME == model.UserName && m.MATKHAU == model.Password).FirstOrDefault();
                return Ok(new ResponseCode { code = "success", message = "Lấy thông tin user", data = data });
            }
            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [SYS_USER] -  UserName={0},Password={1},ex={2}", model.UserName, model.Password, ex.Message);
                return Json(new ResponseCode { code = "error", message = err });
            }
        }
        /// <summary>
        /// Lấy thông tin user khi đăng nhập.
        /// </summary>
        [HttpPost]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(GetSysUserDTO model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserName))
                {
                    return Json(new ResponseCode { code = "error", message = "UserName không được bỏ trống" });
                }
                if (string.IsNullOrEmpty(model.Password))
                {
                    return Json(new ResponseCode { code = "error", message = "Mật khẩu không được bỏ trống" });
                }
                await Task.Delay(1000);
                var data = _db.SYS_USER.Where(m => m.USERNAME == model.UserName && m.MATKHAU == model.Password).FirstOrDefault();
                return Ok(new ResponseCode { code = "success", message = "Lấy thông tin user", data = data });
            }
            catch (SqlException ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [SYS_USER] -  UserName={0},Password={1},ex={2}", model.UserName, model.Password, ex.Message);
                return Json(new ResponseCode { code = "error", message = err });
            }
        }

        /// <summary>
        /// Danh sách các khiếu nại user.
        /// </summary>
        [HttpGet]
        [Route("GetListKhieuNaiUser")]
        public async Task<IHttpActionResult> GetListKhieuNaiUser(string username, string idkhieunai)
        {
            GetKhieuNaiUserDTO model = new GetKhieuNaiUserDTO()
            {
                UserName = username,
                IdKhieuNai = idkhieunai
            };
            try
            {
                await Task.Delay(1000);
                if (string.IsNullOrEmpty(model.IdKhieuNai) && string.IsNullOrEmpty(model.UserName))
                {
                    return Ok(new ResponseCode { code = "error", message = "Phải nhập mã số khiếu nại hoặc UserName" });
                }
                var data = KhieuNaiBLL.GetListKhieuNaiUser(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                string err = string.Format("[ERR_KhieuNai] loi get du lieu [DT_KHIEUNAI_] -  UserName={0},IdKhieuNai={1},ex={2}", model.UserName, model.IdKhieuNai, ex.Message);
                return Json(new ResponseCode { code = "error", message = err });
            }
        }
        #endregion
    }
}
