using LoginManagement.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginManagement.DataService.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public virtual  DbSet<Student> Students { get; set; }
        public virtual DbSet<Coordinator> Coordinators { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            

            modelBuilder.Entity<ApplicationUser>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<ApplicationUser>("ApplicationUser")
                .HasValue<Student>("Student")
                .HasValue<Coordinator>("Coordinator");


            // Seed data (optional)
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Student", NormalizedName = "STUDENT" },
                new ApplicationRole { Id = Guid.NewGuid(), Name = "Coordinator", NormalizedName = "COORDINATOR" },
                 new ApplicationRole { Id = Guid.NewGuid(), Name = "Work Admin", NormalizedName = "WORK ADMIN" },
        new ApplicationRole { Id = Guid.NewGuid(), Name = "Staff Admin", NormalizedName = "STAFF ADMIN" }
            );
        }
    }

}
