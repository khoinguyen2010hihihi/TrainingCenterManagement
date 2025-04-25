using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Controllers
{
    public class HocViensController : Controller
    {
        private TrainingCenterContext db = new TrainingCenterContext();

        // GET: HocViens
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["VaiTro"]?.ToString() == "Admin")
            {
                base.OnActionExecuting(filterContext);
            }
            else if (Session["VaiTro"]?.ToString() != "HocVien")
            {
                filterContext.Result = new RedirectResult("/TaiKhoan/DangNhap");
            }
        }

        public ActionResult Index()
        {
            return View(db.HocViens.ToList());
        }

        // GET: HocViens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocVien hocVien = db.HocViens.Find(id);
            if (hocVien == null)
            {
                ViewBag.Message = "❌ Học viên không tồn tại!";
                ViewBag.RedirectTo = "HocViens";
                return View("Error");
            }
            return View(hocVien);
        }

        // GET: HocViens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HocViens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHocVien,HoTen,NgaySinh,SoDienThoai,Email,TaiKhoan,MatKhau")] HocVien hocVien)
        {
            hocVien.VaiTro = "HocVien";
            if (ModelState.IsValid)
            {
                db.HocViens.Add(hocVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hocVien);
        }

        // GET: HocViens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocVien hocVien = db.HocViens.Find(id);
            if (hocVien == null)
            {
                return HttpNotFound();
            }
            return View(hocVien);
        }

        // POST: HocViens/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHocVien,HoTen,NgaySinh,SoDienThoai,Email,TaiKhoan,MatKhau,VaiTro")] HocVien hocVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hocVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hocVien);
        }

        // GET: HocViens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HocVien hocVien = db.HocViens.Find(id);
            if (hocVien == null)
            {
                return HttpNotFound();
            }
            return View(hocVien);
        }

        // POST: HocViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HocVien hocVien = db.HocViens.Find(id);
            db.HocViens.Remove(hocVien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
