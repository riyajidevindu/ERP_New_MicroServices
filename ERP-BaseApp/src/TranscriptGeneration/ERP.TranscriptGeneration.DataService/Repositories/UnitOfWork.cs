using ERP.TranscriptGenetation.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace ERP.TranscriptGeneration.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IStudentRepository Students { get; }

        public IResultRepository Results { get; }


        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");
            Students = new StudentRepository(_context, logger);

            Results = new ResultRepository(_context, logger);

        }

        public async Task<bool> CompleteAsync()
        {
            var results = await _context.SaveChangesAsync();
            return results > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
