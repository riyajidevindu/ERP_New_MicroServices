using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabEquipmentManagement.DataService.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ILabEquipmentRepository LabEquipments {  get; }
        Task<bool> CompleteAsync();
       // object Update(Guid labEquipmentId, object labEquipment);
    }
}
