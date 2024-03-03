using ERP.StudentRegistration.Core.Entity;
using ERP.StudentRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;

using Microsoft.Extensions.Logging;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class LecturerRepository : GenericRepository<Lecturer>, ILecturerRepository
    {
        public LecturerRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
