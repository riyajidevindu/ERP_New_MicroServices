using ERP.LabEquipmentManagement.Core.Entities;
using ERP.LabEquipmentManagement.DataService.Data;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.DataService.Repositories
{
    public class LabEquipmentRepository : GenericRepository<LabEquipment>, ILabEquipmentRepository
    {
        public LabEquipmentRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {}

        public override async Task<IEnumerable<LabEquipment>> All()
        {
            try
            {
                return await _dbSet.Where(x => x.Status == 1)
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.AddDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(LabEquipmentRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(LabEquipmentRepository));
                throw;
            }
        }

        public override async Task<bool> Update(LabEquipment labEquipment)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == labEquipment.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.Location = labEquipment.Location;
                result.Price = labEquipment.Price;
                result.Description = labEquipment.Description;
                result.EquipmentName = labEquipment.EquipmentName;
                result.EquipmentRegisterId = labEquipment.EquipmentRegisterId;
                result.IsActive = labEquipment.IsActive;
                result.PurchasedDate = labEquipment.PurchasedDate;
                result.SelectCategory = labEquipment.SelectCategory;
              
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(LabEquipmentRepository));
                throw;
            }
        }
    }
}
