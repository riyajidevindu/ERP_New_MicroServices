using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetApprovalRequestResponse
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } // Combining FirstName and LastName
        public int RegisterNumber { get; set; }
        public string Department { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Status { get; set; } // e.g., Pending, Approved, Rejected
        public Guid? ApprovedById { get; set; }
        public string ApprovedByName { get; set; } // Combining FirstName and LastName of the approver
        
    }
}
