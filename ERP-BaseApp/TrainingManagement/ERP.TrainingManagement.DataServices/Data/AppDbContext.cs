using ERP.TrainingManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERP.TrainingManagement.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Coordinator> Coordinators { get; set; }
        public virtual DbSet<InternshipVacancy> InternshipVacancies { get; set; }
        public virtual DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public virtual DbSet<CVUpload> CVUploads { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define relationships and configure properties here if needed
            modelBuilder.Entity<Student>()
                .HasMany(s => s.ApprovalRequests)
                .WithOne(ar => ar.Student)
                .HasForeignKey(ar => ar.StudentId);

            modelBuilder.Entity<Student>()
                .HasMany(s => s.CVUploads)
                .WithOne(cv => cv.Student)
                .HasForeignKey(cv => cv.StudentId);


            modelBuilder.Entity<Coordinator>()
                .HasMany(c => c.ApprovedRequests)
                .WithOne(ar => ar.ApprovedBy)
                .HasForeignKey(ar => ar.ApprovedById);
        }
    }
}
