using ERP.StudentRegistration.Core.Entity;
using ERP.StudentRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;

using Microsoft.Extensions.Logging;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class SemesterRepository : GenericRepository<Semester>, ISemesterRepository
    {
        public SemesterRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

       
    }
}
