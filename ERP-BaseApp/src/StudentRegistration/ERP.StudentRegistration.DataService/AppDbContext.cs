using ERP.StudentRegistration.Core.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.DataService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Advisor)
                .WithMany(a => a.Advicees)
                .HasForeignKey(s => s.AdvisorId)
                .OnDelete(DeleteBehavior.NoAction);
            
            
            
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<Degree> Degrees { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
    }
}
