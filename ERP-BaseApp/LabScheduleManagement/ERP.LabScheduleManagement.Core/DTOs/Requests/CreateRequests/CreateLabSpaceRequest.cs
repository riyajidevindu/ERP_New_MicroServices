using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests
{
    public class CreateLabSpaceRequest
    {
        public string SpaceName { get; set; }
        public string Floor { get; set; }
        public string TableNumber { get; set; }
    }
}
