using ERP.LabScheduleManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.DataServices.Repositories.Interfaces
{
    public interface ILabRepository : IGenericRepository<Lab>
    {
        Task<Lab?> GetModuleLabAsync(Guid moduleId);
    }
}
