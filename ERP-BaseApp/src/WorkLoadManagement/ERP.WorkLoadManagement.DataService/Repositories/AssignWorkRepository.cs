using ERP.WorkLoadManagement.Core.Entities;
using ERP.WorkLoadManagement.DataService.Data;
using ERP.WorkLoadManagement.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.DataService.Repositories
{
    public class AssignWorkRepository : GenericRepository<AssignWork>, IAssignWorkRepository
    {
        public AssignWorkRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
            
        }

        public override async Task<IEnumerable<AssignWork>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(AssignWorkRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(AssignWorkRepository));
                throw;
            }
        }

        public override async Task<bool> Update(AssignWork assignWork)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == assignWork.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.WorkId = assignWork.WorkId;
                result.StaffId = assignWork.StaffId;
                result.Duration = assignWork.Duration;
                result.AssignedDate = assignWork.AssignedDate;
                result.AssignByUserId = assignWork.AssignByUserId;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(AssignWorkRepository));
                throw;
            }
        }
    }
}
