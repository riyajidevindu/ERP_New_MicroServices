using ERP.WorkLoadManagement.DataService.Data;
using ERP.WorkLoadManagement.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IWorkRepository Works { get; }

        public IStaffRepository Staffs { get; }
        public IAssignWorkRepository AssignWorks { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            Works = new WorkRepository(_context, logger);
            Staffs = new StaffRepository(_context, logger);
            AssignWorks = new AssignWorkRepository(_context, logger);


        }
        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
