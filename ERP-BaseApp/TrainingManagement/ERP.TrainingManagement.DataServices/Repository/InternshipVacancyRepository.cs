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
    public class InternshipVacancyRepository : GenericRepository<InternshipVacancy>,IInternshipVacancyRepository
       
    {
        public InternshipVacancyRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<bool> Update(InternshipVacancy internship)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == internship.Id);
                if (result == null)
                {
                    return false;
                }
                result.status=internship.status;
                result.CreatedDate=internship.CreatedDate;
                result.ModifiedDate=internship.ModifiedDate;
                result.Description=internship.Description;
                result.Title=internship.Title;
                result.Company=internship.Company;
                


          


                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error", typeof(InternshipVacancyRepository));
                throw;
            }
        }
    }
}
