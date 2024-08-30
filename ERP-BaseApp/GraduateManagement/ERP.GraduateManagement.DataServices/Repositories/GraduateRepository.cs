using ERP.GraduateManagement.Core.Entities;
using ERP.GraduateManagement.DataServices.Data;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.GraduateManagement.DataServices.Repositories
{
    public class GraduateRepository : GenericRepository<Graduate>, IGraduateRepository
    {
        public GraduateRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }

        public override async Task<IEnumerable<Graduate>> All()
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
                _logger.LogError(ex, "{Repo} All function error", typeof(GraduateRepository));
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
                _logger.LogError(ex, "{Repo} Delete function error", typeof(GraduateRepository));
                throw;
            }
        }

        public override async Task<bool> Update(Graduate graduate)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == graduate.Id);
                if (result == null)
                {
                    return false;
                }
                result.UpdateDate = DateTime.UtcNow;
                result.RegNo = graduate.RegNo;
                result.FirstName = graduate.FirstName;
                result.LastName = graduate.LastName;
                result.ContactNo = graduate.ContactNo;
                result.Email = graduate.Email;
                result.Degree = graduate.Degree;
                result.CurrentJobPosition = graduate.CurrentJobPosition;
                result.CurrentCompany = graduate.CurrentCompany;


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(GraduateRepository));
                throw;
            }
        }
    }
}
