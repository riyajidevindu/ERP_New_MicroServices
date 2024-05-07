using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll
{
    public class GetLabResponse
    {
        public Guid LabId { get; set; }
        public string LabName { get; set; }
        public string LabDescription { get; set; }
        public Guid? ModuleId { get; set; }
    }
}
