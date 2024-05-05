using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests
{
    public class CreateLabCoordinatorRequest
    {
        public string CoordinatorName { get; set; }
        public string EmailAddress { get; set; }
    }
}
