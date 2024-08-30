using ERP.TrainingManagement.Core.Entities;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.TrainingManagement.DataServices.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ERP.TrainingManagement.DataServices.Data;


namespace ERP.TrainingManagement.DataServices.Repository
{
    public class StudentManagementRepository : GenericRepository<ApplicationUser>, IStudentManagementRepository
    {
        public StudentManagementRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

            
        }

        public Task<ApplicationUser> GetStudentByFirstNameAsync(string firstName)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.FirstName == firstName);
        }

        Task<Student?> IStudentManagementRepository.GetStudentByRegisterNumber(int registerNumber)
        {
            
            throw new NotImplementedException();
        }
    }
    
}
