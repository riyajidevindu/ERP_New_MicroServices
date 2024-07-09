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
        Task<IEnumerable<CVUpload>> GetCvsByStudentIdAsync(Guid studentId);
        Task AddRegistrationLetter(RegistartionLetterUpload registrationLetterUpload);
        Task AddCv(CVUpload cvUpload);
        Task<IEnumerable<RegistartionLetterUpload>> GetRegistrationLettersByStudentIdAsync(Guid studentId);

        Task<List<CVUpload>> GetCVUploadsByVacancyIdAsync(Guid vacancyId);
    }
}
