using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess;
namespace ApiProject.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            //var data = KhieuNaiBLL.GetListSYSCONBySTTTinh("70");
            //int row = data.Rows.Count;
            return View();
        }
	}
}