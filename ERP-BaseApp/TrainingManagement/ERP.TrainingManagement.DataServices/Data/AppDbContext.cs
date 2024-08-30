using ERP.TrainingManagement.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ERP.TrainingManagement.DataServices.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Coordinator> Coordinators { get; set; }


        public virtual DbSet<InternshipVacancy> InternshipVacancies { get; set; }
        public virtual DbSet<ApprovalRequest> ApprovalRequests { get; set; }
        public virtual DbSet<CVUpload> CVUploads { get; set; }

        public virtual DbSet<RegistartionLetterUpload> RegistrationLetterUploads { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

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

            modelBuilder.Entity<CVUpload>()
                .HasOne(c => c.InternshipVacancy)
                .WithMany(v => v.CVUploads)
                .HasForeignKey(c => c.VacancyId);



            modelBuilder.Entity<Student>()
                .HasMany(s => s.Registers)
                .WithOne(rl => rl.Student)
                .HasForeignKey(rl => rl.StudentId);

            modelBuilder.Entity<Coordinator>()
                .HasMany(c => c.ApprovedRequests)
                .WithOne(ar => ar.ApprovedBy)
                .HasForeignKey(ar => ar.ApprovedById);

            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Student>("Student")
                .HasValue<Coordinator>("Coordinator");


            // Seed data (optional)
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Student", NormalizedName = "STUDENT" },
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Coordinator", NormalizedName = "COORDINATOR" }
            );
        }
    }
}
