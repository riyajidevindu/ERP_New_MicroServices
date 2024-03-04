using ERP.ModuleRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;
using Microsoft.Extensions.Logging;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IStudentRepository Students { get; }
        public IModuleOfferingRepository ModuleOfferings { get; }
        public IModuleRepository Modules { get; }
        public ILecturerRepository Lecturers { get; }



        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");
            Students = new StudentRepository(_context, logger);
            Modules = new ModuleRepository(_context,logger);
            ModuleOfferings = new ModuleOfferingRepository(_context,logger);    
            Lecturers = new LecturerRepository(_context,logger);                       
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
