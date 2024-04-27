using ERP.LabScheduleManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Repositories.Interfaces
{
    public interface IScheduledLabRepository : IGenericRepository<ScheduledLab>
    {
        Task<ScheduledLab?> GetInstructorScheduledLabAsync(Guid instructorId);
        Task<ScheduledLab?> GetCoordinatorScheduledLabAsync(Guid coordinatorId);
        Task<ScheduledLab?> GetLabScheduledLabAsync(Guid labId);
        Task<ScheduledLab?> GetTimeSlotScheduledLabAsync(Guid timeSlotId);
        Task<ScheduledLab?> GetLabSpaceScheduledLabAsync(Guid labSpaceId);
        Task<ScheduledLab?> GetLabGroupScheduledLabAsync(Guid labGroupId);
    }
}
