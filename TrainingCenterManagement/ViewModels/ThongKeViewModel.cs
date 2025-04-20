using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingCenterManagement.ViewModels
{
    public class ThongKeViewModel
    {
        public string TenKhoaHoc { get; set; }
        public int SoLuongDangKy { get; set; }
    }

    public class ThongKeDoanhThuViewModel
    {
        public string TenKhoaHoc { get; set; }
        public int SoLuongDangKy { get; set; }
        public decimal HocPhi { get; set; }
        public decimal DoanhThu { get; set; }

        public DateTime? NgayDangKy { get; set; }
    }
}