using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrainingCenterManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Hệ thống quản lí trung tâm đào tạo Khôi Bé";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Liên Hệ Khôi Bé";

            return View();
        }
    }
}