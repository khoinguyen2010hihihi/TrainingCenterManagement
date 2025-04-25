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
    public class KhoaHocsController : Controller
    {
        private TrainingCenterContext db = new TrainingCenterContext();

        // GET: KhoaHocs
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["VaiTro"]?.ToString() != "Admin")
            {
                filterContext.Result = new RedirectResult("/TaiKhoan/DangNhap");
            }
            base.OnActionExecuting(filterContext);
        }
        public ActionResult Index()
        {
            return View(db.KhoaHocs.ToList());
        }

        // GET: KhoaHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                ViewBag.Message = "❌ Khóa học không tồn tại!";
                ViewBag.RedirectTo = "KhoaHocs";
                return View("Error");
            }
            return View(khoaHoc);
        }

        // GET: KhoaHocs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: KhoaHocs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKhoaHoc,TenKhoaHoc,GiangVien,HocPhi,ThoiGianKhaiGiang,SoLuongToiDa")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.KhoaHocs.Add(khoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(khoaHoc);
        }

        // GET: KhoaHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // POST: KhoaHocs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKhoaHoc,TenKhoaHoc,GiangVien,HocPhi,ThoiGianKhaiGiang,SoLuongToiDa")] KhoaHoc khoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(khoaHoc);
        }

        // GET: KhoaHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            if (khoaHoc == null)
            {
                return HttpNotFound();
            }
            return View(khoaHoc);
        }

        // POST: KhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhoaHoc khoaHoc = db.KhoaHocs.Find(id);
            db.KhoaHocs.Remove(khoaHoc);
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
