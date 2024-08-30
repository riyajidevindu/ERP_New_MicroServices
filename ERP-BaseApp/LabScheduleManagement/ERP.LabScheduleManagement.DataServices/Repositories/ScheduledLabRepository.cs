using ERP.LabScheduleManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using ERP.LabScheduleManagement.DataServices.Data;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace ERP.LabScheduleManagement.DataServices.Repositories
{
    public class ScheduledLabRepository : GenericRepository<ScheduledLab>, IScheduledLabRepository
    {
        public ScheduledLabRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<ScheduledLab?> GetCoordinatorScheduledLabAsync(Guid coordinatorId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.LabCoordinatorId == coordinatorId);

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "{Repo} GetCoordinatorScheduledLabAsync function error", typeof(ScheduledLabRepository));
                throw;
            } 
        }

        public async Task<ScheduledLab?> GetInstructorScheduledLabAsync(Guid instructorId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.LabInstructorId == instructorId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetInstructorScheduledLabAsync function error", typeof(ScheduledLabRepository));
                throw;
            }
        }

        public async Task<ScheduledLab?> GetLabGroupScheduledLabAsync(Guid labGroupId)
        {
            throw new NotImplementedException();
        }

        public async Task<ScheduledLab?> GetLabScheduledLabAsync(Guid labId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.LabId == labId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetLabScheduledLabAsync function error", typeof(ScheduledLabRepository));
                throw;
            }
        }

        public async Task<ScheduledLab?> GetLabSpaceScheduledLabAsync(Guid labSpaceId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.LabSpaceId == labSpaceId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetLabSpaceScheduledLabAsync function error", typeof(ScheduledLabRepository));
                throw;
            }
        }

        public async Task<ScheduledLab?> GetTimeSlotScheduledLabAsync(Guid timeSlotId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.TimeSlotId == timeSlotId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetTimeSlotScheduledLabAsync function error", typeof(ScheduledLabRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<ScheduledLab>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(ScheduledLabRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ScheduledLabRepository));
                throw;
            }
        }

        public override async Task<bool> Update(ScheduledLab scheduledLab)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == scheduledLab.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.Resheduled = scheduledLab.Resheduled;
                result.Completed = scheduledLab.Completed;
                result.LabInstructorId = scheduledLab.LabInstructorId;
                result.LabCoordinatorId = scheduledLab.LabCoordinatorId;
                result.LabId = scheduledLab.LabId;
                result.TimeSlotId = scheduledLab.TimeSlotId;
                result.LabSpaceId = scheduledLab.LabSpaceId;
                //result.LabEquipmets = scheduledLab.LabEquipmets;
                //result.LabGroups = scheduledLab.LabGroups;
         


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(ScheduledLabRepository));
                throw;
            }
        }
    }
}
