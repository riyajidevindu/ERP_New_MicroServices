using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests
{
    public class UpdateLabEquipmentRequest
    {
        public Guid LabEquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
        public Guid? ScheduledLabId { get; set; }
    }
}
