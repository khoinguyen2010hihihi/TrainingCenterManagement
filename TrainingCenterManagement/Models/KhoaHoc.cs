using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace TrainingCenterManagement.Models
{
    public class KhoaHoc
    {
        [Key]
        public int MaKhoaHoc { get; set; }

        [Required]
        public string TenKhoaHoc { get; set; }

        public string GiangVien { get; set; }

        public decimal HocPhi { get; set; }

        public DateTime ThoiGianKhaiGiang { get; set; }

        public int SoLuongToiDa { get; set; }

        public virtual ICollection<DangKyKhoaHoc> DangKyKhoaHocs { get; set; }
    }
}