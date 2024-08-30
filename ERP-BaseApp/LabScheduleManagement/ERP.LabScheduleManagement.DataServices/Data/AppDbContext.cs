using ERP.LabScheduleManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        //Define the DB Entities
        public virtual DbSet<ScheduledLab> ScheduledLabs { get; set; }
        public virtual DbSet<LabInstructor> LabInstructors { get; set;}
        public virtual DbSet<LabCoordinator> LabCoordinators { get; set; }
        public virtual DbSet<Lab> Labs { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<TimeSlot> TimeSlots { get; set; }
        public virtual DbSet<LabSpace> LabSpaces { get; set; }
        public virtual DbSet<LabEquipment> LabEquipments { get; set; }
        public virtual DbSet<LabGroup> LabGroups { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //specify the relationship between the entities
            modelBuilder.Entity<ScheduledLab>(entity => 
                {
                    entity.HasOne(d => d.LabInstructor)
                        .WithMany(p => p.ScheduledLabs)
                        .HasForeignKey(d => d.LabInstructorId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_ScheduledLabs_LabInstructor");
                }
            );

            modelBuilder.Entity<ScheduledLab>(entity =>
                {
                    entity.HasOne(d => d.LabCoordinator)
                        .WithMany(p => p.ScheduledLabs)
                        .HasForeignKey(d => d.LabCoordinatorId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_ScheduledLabs_LabCoordinator");
                }
            );

            modelBuilder.Entity<ScheduledLab>(entity =>
                {
                    entity.HasOne(d => d.Lab)
                        .WithMany(p => p.ScheduledLabs)
                        .HasForeignKey(d => d.LabId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_ScheduledLabs_Lab");
                }
            );

            modelBuilder.Entity<Lab>(entity => 
                {
                    entity.HasOne(d => d.Module)
                        .WithMany(p => p.Labs)
                        .HasForeignKey(d => d.ModuleId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Lab_Module");
                }
            );

            modelBuilder.Entity<ScheduledLab>(entity =>
                {
                    entity.HasOne(d => d.TimeSlot)
                        .WithMany(p => p.ScheduledLabs)
                        .HasForeignKey(d => d.TimeSlotId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_ScheduledLabs_TimeSlot");
                }
            );

            modelBuilder.Entity<ScheduledLab>(entity =>
                {
                    entity.HasOne(d => d.LabSpace)
                        .WithMany(p => p.ScheduledLabs)
                        .HasForeignKey(d => d.LabSpaceId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_ScheduledLabs_LabSpace");
                }
            );

            modelBuilder.Entity<LabEquipment>(entity =>
                {
                    entity.HasOne(d => d.ScheduledLab)
                        .WithMany(p => p.LabEquipmets)
                        .HasForeignKey(d => d.ScheduledLabId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_LabEquipments_ScheduledLab");
                }
            );

            modelBuilder.Entity<Student>(entity =>
                {
                    entity.HasOne(d => d.LabGroup)
                        .WithMany(p => p.Students)
                        .HasForeignKey(p => p.LabGroupId)
                        .OnDelete(DeleteBehavior.NoAction)
                        .HasConstraintName("FK_Students_LabGroup");

                }
            );

            modelBuilder.Entity<ScheduledLab>()
             .HasMany(sl => sl.LabGroups)
             .WithMany(lg => lg.ScheduledLabs)
             .UsingEntity("ScheduledLabLabGroups");

        }
    }
}
 