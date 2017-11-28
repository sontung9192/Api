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

namespace ApiProject.Controllers
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        QL_KHIEUNAIEntities db = new QL_KHIEUNAIEntities();
       
        //[HttpGet]
        //[Route("List")]
        ///// <summary>
        ///// Lấy 1 danh sách test.
        ///// </summary>
        //public async Task<IHttpActionResult> ListTest()
        //{
        //    var data = db.Tests.ToList();
        //    return Json(data);
        //}
        ///// <summary>
        ///// Thêm danh sách test.
        ///// </summary>
        //[HttpPost]
        //[Route("insertTest")]
        //public async Task<IHttpActionResult> TestModify(Test model)
        //{
        //    try
        //    {
        //        //ApiProject.Models.Test data = new ApiProject.Models.Test()
        //        //{
        //        //    Id = model.Id,
        //        //    Names = model.Names,
        //        //    Datetime = DateTime.Now,
        //        //    Status = model.Status
        //        //};
        //        var data = model;
        //        if (model.Id > 0)
        //        {
        //            var record = db.Tests.Where(m => m.Id == model.Id).FirstOrDefault();
        //            if (record == null)
        //            {
        //                return Ok(new ResponseInfo { Code = -1, ResponseMessage = "Id này không tồn tại" });
        //            }
        //            record.Names = model.Names;
        //            record.Datetime = model.Datetime == null ? DateTime.Now : model.Datetime;
        //            record.Status = model.Status;
        //            db.SaveChanges();
        //            return Ok(new ResponseInfo { Code = 1, ResponseMessage = "Cập nhật thông tin thành công" });
        //        }
        //        else
        //        {
        //            db.Tests.Add(data);
        //            db.SaveChanges();
        //            return Ok(new ResponseInfo { Code = 2, ResponseMessage = "Thêm dữ liệu thành công" });
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string err = string.Format("[ERR_KhieuNai] ex = {0},id={1},names={2}", ex.Message, model.Id, model.Names);
        //        LogHelper.LogInfo(err, "KhieuNai");
        //        return Ok(new ResponseInfo { Code = -999, ResponseMessage = "Lỗi trong quá trình xử lý" });
        //    }
        //}
    }
}
