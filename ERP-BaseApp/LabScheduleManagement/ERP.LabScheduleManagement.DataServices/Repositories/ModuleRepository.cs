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
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Module>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(ModuleRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ModuleRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Module module)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == module.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.ModuleName = module.ModuleName;
                result.ModuleCoordinator = module.ModuleCoordinator;
                result.NoofLabs = module.NoofLabs;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(ModuleRepository));
                throw;
            }
        }
    }
}
