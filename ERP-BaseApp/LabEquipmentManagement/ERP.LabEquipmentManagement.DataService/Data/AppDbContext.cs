using ERP.LabEquipmentManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.DataService.Data
{
    public class AppDbContext : DbContext
    {
        public virtual DbSet<LabEquipment> LabEquipment { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);


        }
    }
}
