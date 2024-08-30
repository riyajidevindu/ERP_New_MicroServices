using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.DataServices.Data;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Repositories
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public StudentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Student?> GetLabGroupStudentAsync(Guid labGroupId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.LabGroupId == labGroupId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetLabGroupStudentAsync function error", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<Student>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                //get my enntity
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                result.Status = 0;
                result.UpdateDate = DateTime.UtcNow;
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(StudentRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Student student)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == student.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.Name = student.Name;
                result.RegNo = student.RegNo;
                result.present = student.present;
                result.LabGroupId = student.LabGroupId;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(StudentRepository));
                throw;
            }
        }
    }
}
