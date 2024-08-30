using ERP.ModuleRegistration.DataService.Repositories;
using ERP.ModuleRegistration.Core.Entity;
using ERP.StudentRegistration.DataService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ERP.ModuleRegistration.Core.Interfaces;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }


        public override async Task<bool> UpdateAsync(Student entity)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (result == null)
                    return false;

                result.DateOfBirth = entity.DateOfBirth;
                result.Status = entity.Status;
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.RegisteredDate = entity.RegisteredDate;
                result.UpdateDate = DateTime.UtcNow;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error",
                    typeof(StudentRepository));
                throw;
            }
        }
    }
}
