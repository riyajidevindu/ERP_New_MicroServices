using ERP.GraduateManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.GraduateManagement.DataServices.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<Graduate> Graduates { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


        }

    }


}

