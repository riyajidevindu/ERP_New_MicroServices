using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.TrainingManagement.DataServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.TrainingManagement.Core.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ERP.TrainingManagement.DataServices.Data;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;

namespace ERP.TrainingManagement.DataServices.Repository
{
    public class ApprovalRequestsRepository: GenericRepository<ApprovalRequest>,IApprovalRequestRepository
    {
        public ApprovalRequestsRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

                
        }
        public override async Task<bool> Update(ApprovalRequest approvalRequest)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == approvalRequest.Id);
                if (result == null)
                {
                    return false;
                }
                result.status = approvalRequest.status;
                result.CreatedDate = approvalRequest.CreatedDate;
                result.ModifiedDate = approvalRequest.ModifiedDate;
                result.Company = approvalRequest.Company;
                result.StudentId = approvalRequest.StudentId;
                result.ApprovedById = approvalRequest.ApprovedById;

             
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(ApprovalRequestsRepository));
                throw;
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
                if (result == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(ApprovalRequestsRepository));
                throw;
            }
        }

        public override async Task<IEnumerable<ApprovalRequest>> All()
        {
            try
            {
                return await _dbSet
                    .AsNoTracking()
                    .AsSplitQuery()
                    .OrderBy(x => x.CreatedDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(ApprovalRequestsRepository));
                throw;
            }
        }

        //public async Task<IEnumerable<ApprovalRequest>> GetByStatus(int status)
        //{
        //    try
        //    {
        //        return await _dbSet
        //            .Where(x => x.Status == status)
        //            .AsNoTracking()
        //            .ToListAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "{Repo} GetByStatus function error", typeof(ApprovalRequestsRepository));
        //        throw;
        //    }
        //}
    }
}
