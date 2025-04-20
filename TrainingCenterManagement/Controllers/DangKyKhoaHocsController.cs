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
    public class DangKyKhoaHocsController : Controller
    {
        private TrainingCenterContext db = new TrainingCenterContext();

        // GET: DangKyKhoaHocs
        public ActionResult Index()
        {
            var dangKyKhoaHocs = db.DangKyKhoaHocs.Include(d => d.HocVien).Include(d => d.KhoaHoc);
            return View(dangKyKhoaHocs.ToList());
        }

        // GET: DangKyKhoaHocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DangKyKhoaHoc dangKyKhoaHoc = db.DangKyKhoaHocs.Find(id);
            if (dangKyKhoaHoc == null)
                return HttpNotFound();

            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHocs/Create
        public ActionResult Create()
        {
            var vaiTro = Session["VaiTro"]?.ToString();

            if (vaiTro == "HocVien")
            {
                ViewBag.IsHocVien = true;
                ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc");
            }
            else
            {
                ViewBag.IsHocVien = false;
                ViewBag.MaHocVien = new SelectList(db.HocViens, "MaHocVien", "HoTen");
                ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc");
            }

            return View();
        }

        // POST: DangKyKhoaHocs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDangKy,MaHocVien,MaKhoaHoc,NgayDangKy")] DangKyKhoaHoc dangKyKhoaHoc)
        {
            var vaiTro = Session["VaiTro"]?.ToString();

            if (vaiTro == "HocVien")
            {
                // Gán học viên hiện tại ngay từ đầu
                string taiKhoan = Session["TaiKhoan"]?.ToString();
                var hocVien = db.HocViens.FirstOrDefault(hv => hv.TaiKhoan == taiKhoan);
                if (hocVien != null)
                {
                    dangKyKhoaHoc.MaHocVien = hocVien.MaHocVien;
                    ModelState.Remove("MaHocVien"); // 🛠 Xóa lỗi validation cũ (vì Form gửi lên là 0)
                }
            }

            // Kiểm tra trùng đăng ký
            bool daDangKy = db.DangKyKhoaHocs.Any(d =>
                d.MaHocVien == dangKyKhoaHoc.MaHocVien &&
                d.MaKhoaHoc == dangKyKhoaHoc.MaKhoaHoc);

            if (daDangKy)
            {
                ModelState.AddModelError("", "❌ Học viên đã đăng ký khóa học này rồi.");
            }

            // Kiểm tra giới hạn số lượng
            int soLuongDaDangKy = db.DangKyKhoaHocs.Count(d => d.MaKhoaHoc == dangKyKhoaHoc.MaKhoaHoc);
            int gioiHan = db.KhoaHocs.Find(dangKyKhoaHoc.MaKhoaHoc)?.SoLuongToiDa ?? 0;

            if (soLuongDaDangKy >= gioiHan)
            {
                ModelState.AddModelError("", "❌ Số lượng học viên đã đạt giới hạn.");
            }

            if (ModelState.IsValid)
            {
                db.DangKyKhoaHocs.Add(dangKyKhoaHoc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Gửi lại ViewBag khi lỗi
            if (vaiTro == "HocVien")
            {
                ViewBag.IsHocVien = true;
                ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", dangKyKhoaHoc.MaKhoaHoc);
            }
            else
            {
                ViewBag.IsHocVien = false;
                ViewBag.MaHocVien = new SelectList(db.HocViens, "MaHocVien", "HoTen", dangKyKhoaHoc.MaHocVien);
                ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", dangKyKhoaHoc.MaKhoaHoc);
            }

            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DangKyKhoaHoc dangKyKhoaHoc = db.DangKyKhoaHocs.Find(id);
            if (dangKyKhoaHoc == null)
                return HttpNotFound();

            ViewBag.MaHocVien = new SelectList(db.HocViens, "MaHocVien", "HoTen", dangKyKhoaHoc.MaHocVien);
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", dangKyKhoaHoc.MaKhoaHoc);
            return View(dangKyKhoaHoc);
        }

        // POST: DangKyKhoaHocs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDangKy,MaHocVien,MaKhoaHoc,NgayDangKy")] DangKyKhoaHoc dangKyKhoaHoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dangKyKhoaHoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHocVien = new SelectList(db.HocViens, "MaHocVien", "HoTen", dangKyKhoaHoc.MaHocVien);
            ViewBag.MaKhoaHoc = new SelectList(db.KhoaHocs, "MaKhoaHoc", "TenKhoaHoc", dangKyKhoaHoc.MaKhoaHoc);
            return View(dangKyKhoaHoc);
        }

        // GET: DangKyKhoaHocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            DangKyKhoaHoc dangKyKhoaHoc = db.DangKyKhoaHocs.Find(id);
            if (dangKyKhoaHoc == null)
                return HttpNotFound();

            return View(dangKyKhoaHoc);
        }

        // POST: DangKyKhoaHocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DangKyKhoaHoc dangKyKhoaHoc = db.DangKyKhoaHocs.Find(id);
            db.DangKyKhoaHocs.Remove(dangKyKhoaHoc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}
