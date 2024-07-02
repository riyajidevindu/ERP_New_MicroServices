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
    }
}
