using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TrainingCenterManagement.Data;
using TrainingCenterManagement.ViewModels;

namespace TrainingCenterManagement.Controllers
{
    public class ThongKeController : Controller
    {
        private TrainingCenterContext db = new TrainingCenterContext();

        // Thống kê số học viên theo từng khóa học (ai cũng xem được)
        public ActionResult Index()
        {
            var thongKe = db.KhoaHocs
                .Select(kh => new ThongKeViewModel
                {
                    TenKhoaHoc = kh.TenKhoaHoc,
                    SoLuongDangKy = kh.DangKyKhoaHocs.Count()
                })
                .ToList();

            return View(thongKe);
        }

        // Thống kê doanh thu theo khóa học (chỉ Admin xem)
        public ActionResult DoanhThu()
        {
            if (Session["VaiTro"]?.ToString() != "Admin")
                return RedirectToAction("DangNhap", "TaiKhoan");

            var data = db.KhoaHocs
                .Select(kh => new ThongKeDoanhThuViewModel
                {
                    TenKhoaHoc = kh.TenKhoaHoc,
                    HocPhi = kh.HocPhi,
                    SoLuongDangKy = kh.DangKyKhoaHocs.Count(),
                    DoanhThu = kh.HocPhi * kh.DangKyKhoaHocs.Count()
                })
                .ToList();

            return View(data);
        }

        public ActionResult DoanhThuTheoThoiGian(DateTime? tuNgay, DateTime? denNgay)
        {
            if (Session["VaiTro"]?.ToString() != "Admin")
                return RedirectToAction("DangNhap", "TaiKhoan");

            var query = db.DangKyKhoaHocs.Include("KhoaHoc").AsQueryable();

            if (tuNgay.HasValue)
                query = query.Where(d => d.NgayDangKy >= tuNgay.Value);

            if (denNgay.HasValue)
                query = query.Where(d => d.NgayDangKy <= denNgay.Value);

            var data = query
                .GroupBy(d => d.KhoaHoc.TenKhoaHoc)
                .Select(g => new ThongKeDoanhThuViewModel
                {
                    TenKhoaHoc = g.Key,
                    SoLuongDangKy = g.Count(),
                    HocPhi = g.FirstOrDefault().KhoaHoc.HocPhi,
                    DoanhThu = g.Sum(x => x.KhoaHoc.HocPhi)
                })
                .ToList();

            ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
            ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

            return View(data);
        }
    }
}