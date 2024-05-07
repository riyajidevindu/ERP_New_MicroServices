using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetById
{
    public class GetLabEquipmentByIdResponse
    {
        public Guid LabEquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public Guid? ScheduledLabId { get; set; }
    }
}
