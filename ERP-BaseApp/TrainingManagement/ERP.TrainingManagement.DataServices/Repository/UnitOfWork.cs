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

        public IFileRepository FileRepository { get; private set; }

        public IInternshipVacancyRepository AddJobRepository { get; }

        public IApprovalRequestRepository AddApprovalRequestRepository {  get; }

        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");

            AddJobRepository = new InternshipVacancyRepository(_context, logger);

            AddApprovalRequestRepository=new ApprovalRequestsRepository(_context, logger);

            FileRepository = new FileRepository(_context);
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
