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
    public class LabGroupRepository : GenericRepository<LabGroup>, ILabGroupRepository
    {
        public LabGroupRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public Task<LabGroup> GetLabScheduleLabGroupAsync(Guid scheduledLabId)
        {
            throw new NotImplementedException();
        }

        public override async Task<IEnumerable<LabGroup>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(LabGroupRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(LabGroupRepository));
                throw;
            }
        }

        public override async Task<bool> Update(LabGroup labGroup)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == labGroup.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.GroupNumber = labGroup.GroupNumber;
                result.Batch = labGroup.Batch;
                result.Specilization = labGroup.Specilization;
                result.NoOfStudents = labGroup.NoOfStudents;
                //result.Students = labGroup.Students;
                //result.ScheduledLabs =  labGroup.ScheduledLabs;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(LabGroupRepository));
                throw;
            }
        }
    }
}
