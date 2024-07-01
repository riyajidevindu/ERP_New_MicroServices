using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class CreateApprovalRequest
    {
        public Guid StudentId { get; set; }
        public string Company { get; set; }
    }
}
