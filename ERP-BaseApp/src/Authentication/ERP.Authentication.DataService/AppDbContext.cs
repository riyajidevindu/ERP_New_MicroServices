using ERP.Authentication.Core.Entity;
using Microsoft.EntityFrameworkCore;



namespace ERP.Authentication.DataService
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserAccount>()
                .HasData(
                 new UserAccount() { UserName = "admin", Password = "admin", Role = "Adminstrator" },
                new UserAccount() { UserName = "user", Password = "user", Role = "User" }
                );

            

        }

        public virtual DbSet<UserAccount> UserAccounts { get; set; }

    }
}
