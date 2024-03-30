using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ILabEquipmentRepository LabEquipment {  get; }
        Task<bool> CompleteAsync();
    }
}
