using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public ILecturerRepository Lecturers { get; }
        public IDegreeRepository Degrees { get; }
        public ISemesterRepository Semesters { get; }
        Task<bool> CompleteAsync();
    }
}
