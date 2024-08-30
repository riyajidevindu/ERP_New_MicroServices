using ERP.StudentRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext _context;

        public IStudentRepository Students { get; }
        public ILecturerRepository Lecturers { get; }
        public IDegreeRepository Degrees { get; }
        public ISemesterRepository Semesters { get; }


        public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");
            Students = new StudentRepository(_context, logger);       
            Lecturers = new LecturerRepository(_context,logger); 
            Semesters = new SemesterRepository(_context,logger);
            Degrees = new DegreeRepository(_context, logger);

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
