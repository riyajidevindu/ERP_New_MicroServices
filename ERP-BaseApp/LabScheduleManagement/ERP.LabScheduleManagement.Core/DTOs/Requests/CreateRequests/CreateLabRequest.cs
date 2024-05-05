using ERP.LabScheduleManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests
{
    public class CreateLabRequest
    {
        public string LabName { get; set; }
        public string LabDescription { get; set; }
        public Guid? ModuleId { get; set; }
    }
}
