using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetById
{
    public class GetModuleByIdResponse
    {
        public Guid ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string ModuleCoordinator { get; set; }
        public int NoofLabs { get; set; }
    }
}
