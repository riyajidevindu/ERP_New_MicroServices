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
    public class LabRepository : GenericRepository<Lab>, ILabRepository
    {
        public LabRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public async Task<Lab?> GetModuleLabAsync(Guid moduleId)
        {
            try
            {
                return await _dbSet.FirstOrDefaultAsync(x => x.ModuleId == moduleId);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} GetModuleLabAsync function error", typeof(LabRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<Lab>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(LabRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(LabRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Lab lab)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == lab.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.LabName = lab.LabName;
                result.LabDescription = lab.LabDescription;
                result.ModuleId = lab.ModuleId;
                //result.ScheduledLabs = lab.ScheduledLabs;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(LabRepository));
                throw;
            }
        }
    }
}
