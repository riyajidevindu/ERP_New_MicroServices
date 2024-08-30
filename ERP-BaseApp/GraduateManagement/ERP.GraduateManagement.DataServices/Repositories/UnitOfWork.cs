using ERP.GraduateManagement.DataServices.Data;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.GraduateManagement.DataServices.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IGraduateRepository GraduateRepo { get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            GraduateRepo = new GraduateRepository(_context, logger);


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
