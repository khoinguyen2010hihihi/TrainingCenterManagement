using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Controllers
{
    public class TaiKhoanController : Controller
    {
        private TrainingCenterContext db = new TrainingCenterContext();

        // GET: TaiKhoan/DangNhap
        public ActionResult DangNhap()
        {
            return View();
        }

        // POST: TaiKhoan/DangNhap
        [HttpPost]
        public ActionResult DangNhap(string taiKhoan, string matKhau)
        {
            var user = db.HocViens.FirstOrDefault(hv => hv.TaiKhoan == taiKhoan && hv.MatKhau == matKhau);
            if (user != null)
            {
                Session["TaiKhoan"] = user.TaiKhoan;
                Session["VaiTro"] = user.VaiTro;
                Session["HoTen"] = user.HoTen;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Loi = "❌ Sai tài khoản hoặc mật khẩu!";
            return View();
        }

        public ActionResult DangXuat()
        {
            Session.Clear();
            return RedirectToAction("DangNhap");
        }
    }
}