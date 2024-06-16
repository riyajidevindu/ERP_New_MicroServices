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
    public class WorkRepository : GenericRepository<Work>, IWorkRepository
    {
        public WorkRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Work>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(WorkRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(WorkRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Work work)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == work.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.WorkName = work.WorkName;
                result.WorkType = work.WorkType;
                result.WorkCode = work.WorkCode;
                result.Description = work.Description;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(WorkRepository));
                throw;
            }
        }


    }
}
