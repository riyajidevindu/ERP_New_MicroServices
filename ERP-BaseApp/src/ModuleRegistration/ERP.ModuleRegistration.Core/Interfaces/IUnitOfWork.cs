using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleRegistration.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IStudentRepository Students { get; }
        public IModuleOfferingRepository ModuleOfferings { get; }
        public IModuleRepository Modules { get; }
        public ILecturerRepository Lecturers { get; }
        Task<bool> CompleteAsync();
    }
}
