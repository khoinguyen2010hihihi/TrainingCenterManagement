using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingCenterManagement.Models
{
    public class DangKyKhoaHoc
    {
        [Key]
        public int MaDangKy { get; set; }

        [ForeignKey("HocVien")]
        public int MaHocVien { get; set; }

        [ForeignKey("KhoaHoc")]
        public int MaKhoaHoc { get; set; }

        public DateTime NgayDangKy { get; set; }

        public virtual HocVien HocVien { get; set; }

        public virtual KhoaHoc KhoaHoc { get; set; }
    }
}