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
        public string Company { get; set; }
        public int Status { get; set; }
        public Guid? ApprovedById { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
