using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TrainingCenterManagement.Models;

namespace TrainingCenterManagement.Data
{
    public class TrainingCenterContext : DbContext
    {
        public TrainingCenterContext() : base("TrainingCenterContext") { }

        public DbSet<KhoaHoc> KhoaHocs { get; set; }
        public DbSet<HocVien> HocViens { get; set; }
        public DbSet<DangKyKhoaHoc> DangKyKhoaHocs { get; set; }
    }
}