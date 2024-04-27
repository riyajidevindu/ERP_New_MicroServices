using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ILabCoordinatorRepository Coordinators { get; }
        ILabEquipmentRepository Equipments { get; }
        ILabGroupRepository Groups { get; }
        ILabInstructorRepository Instructors { get; }
        ILabRepository Labs { get; }
        ILabSpaceRepository Spaces { get; }
        IModuleRepository Modules { get; }
        IScheduledLabRepository ScheduledLabs { get; }
        IStudentRepository Students { get; }
        ITimeSlotRepository TimeSlots { get; }

        Task<bool> CompleteAsync();
    }
}
