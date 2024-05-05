using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll
{
    public class GetLabCoordinatorResponse
    {
        public Guid LabCoordinatorId { get; set; }
        public string CoordinatorName { get; set; }
        public string EmailAddress { get; set; }
    }
}
