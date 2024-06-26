using ERP.WorkLoadManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.DataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Configure the relationship between Work and AssignWork
            modelBuilder.Entity<AssignWork>()
                .HasOne(aw => aw.Work)
                .WithMany(w => w.AssignWorks)
                .HasForeignKey(aw => aw.WorkId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure the relationship between Staff and AssignWork
            modelBuilder.Entity<AssignWork>()
                .HasOne(aw => aw.Staff)
                .WithMany(s => s.AssignWorks)
                .HasForeignKey(aw => aw.StaffId)
                .OnDelete(DeleteBehavior.Restrict);



        }
        public virtual DbSet<Work> Works { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<AssignWork> AssignWorks { get; set; }

    



    }
}
