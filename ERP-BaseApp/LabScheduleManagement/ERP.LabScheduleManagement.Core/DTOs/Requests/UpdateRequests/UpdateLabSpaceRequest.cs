using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests
{
    public class UpdateLabSpaceRequest
    { 
    
        public Guid LabSpaceId {  get; set; }
        public string SpaceName { get; set; }
        public string Floor { get; set; }
        public string TableNumber { get; set; }
    }
}
