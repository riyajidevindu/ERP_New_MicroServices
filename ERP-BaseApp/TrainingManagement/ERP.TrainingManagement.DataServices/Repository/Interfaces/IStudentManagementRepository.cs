﻿using ERP.TrainingManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.DataServices.Repository.Interfaces
{
    public interface IStudentManagementRepository: IGenericRepository<ApplicationUser>
    {
        Task<Student?> GetStudentByRegisterNumber(int registerNumber);
        Task<ApplicationUser> GetStudentByFirstNameAsync(string firstName);
    }
}
