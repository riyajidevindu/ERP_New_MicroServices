using ERP.TranscriptGeneration.DataService;
using ERP.TranscriptGenetation.Core.Entity;
using ERP.TranscriptGenetation.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGeneration.DataService.Repositories
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
                result.FullName = entity.FullName;
                result.DateOfBirth = entity.DateOfBirth;
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
