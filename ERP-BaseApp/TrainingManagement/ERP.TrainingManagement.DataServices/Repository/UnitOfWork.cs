using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using ERP.TrainingManagement.DataServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ERP.TrainingManagement.DataServices.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IInternshipVacancyRepository InternshipRepo { get; }

        public IInternshipVacancyRepository AddJobRepository => throw new NotImplementedException();

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            InternshipRepo = new InternshipVacancyRepository(_context, logger);
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
