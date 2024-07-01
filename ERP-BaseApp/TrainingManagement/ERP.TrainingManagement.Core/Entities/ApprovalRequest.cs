using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class ApprovalRequest : BaseEntity
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public string Company { get; set; }
        public string Status { get; set; } // e.g., Pending, Approved, Rejected

        public Guid? ApprovedById { get; set; }
        public Coordinator ApprovedBy { get; set; }
    }
}
