using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TrainingCenterManagement.Models
{
    public class HocVien
    {
        [Key]
        public int MaHocVien { get; set; }

        public string HoTen { get; set; }

        public DateTime NgaySinh { get; set; }

        public string SoDienThoai { get; set; }

        public string Email { get; set; }

        public string TaiKhoan { get; set; }

        public string MatKhau { get; set; }

        public string VaiTro { get; set; }
    }
}