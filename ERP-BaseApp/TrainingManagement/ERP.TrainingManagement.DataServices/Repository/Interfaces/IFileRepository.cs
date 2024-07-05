using ERP.TrainingManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.DataServices.Repository.Interfaces
{
    public interface IFileRepository 
    {
        Task<CVUpload> GetCvByStudentId(Guid studentId);
        Task<RegistartionLetterUpload> GetRegistrationLetterByStudentId(Guid studentId);
        Task AddCv(CVUpload cvUpload);
        Task AddRegistrationLetter(RegistartionLetterUpload registrationLetterUpload);
    }
}
