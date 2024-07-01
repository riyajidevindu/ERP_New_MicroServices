using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class UpdateApprovalRequest
    {
        public Guid Id { get; set; } // The ID of the approval request to be updated
        public string Status { get; set; } // e.g., Pending, Approved, Rejected
        public Guid? ApprovedById { get; set; }
    }
}
