using ERP.Authentication.Core.Interfaces;
using ERP.Authentication.DataService;
using Microsoft.Extensions.Logging;

namespace ERP.Authentication.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IUserAccountRepository UserAccounts { get; }



        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");
            UserAccounts = new UserAccountRepository(_context, logger);


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
