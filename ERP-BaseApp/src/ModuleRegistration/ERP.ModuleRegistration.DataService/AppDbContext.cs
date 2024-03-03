using ERP.ModuleRegistration.Core.Entity;
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
            
            modelBuilder.Entity<ModuleOffering>()
                .HasOne(m => m.Coordinator)
                .WithMany(l => l.Coordinations)
                .HasForeignKey(m => m.CoordinatorId)
                .OnDelete(DeleteBehavior.NoAction);

            //many to many relationship with join table
            modelBuilder.Entity<ModuleOffering>()
                .HasMany(m => m.FirstExaminers)
                .WithMany(l => l.FirstExaminations)
                .UsingEntity("ModuleOfferingFirstExaminer");

            modelBuilder.Entity<ModuleOffering>()
                .HasMany(m => m.SecondExaminers)
                .WithMany(l => l.SecondExaminations)
                .UsingEntity("ModuleOfferingSecondExaminer");

            modelBuilder.Entity<ModuleOffering>()
                .HasMany(m => m.Moderators)
                .WithMany(l => l.Moderations)
                .UsingEntity("ModuleOfferingModerator");

            modelBuilder.Entity<ModuleOffering>()
                .HasMany(m => m.Students)
                .WithMany(s => s.Modules)
                .UsingEntity("ModuleOfferingStudent");
            
        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<ModuleOffering> ModuleOfferings { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
    }
}
