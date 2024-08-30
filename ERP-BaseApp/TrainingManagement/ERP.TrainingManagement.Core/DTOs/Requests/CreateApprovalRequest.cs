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
        public int Status { get; set; }
        public Guid? ApprovedById { get; set; } 

        public string CompanyAdress { get; set; }

        public string ContactedPerson { get; set; }

        public string FirstName { get; set; }

        public CreateApprovalRequest()
        {
            StudentId = Guid.Parse("1EA0E241-DC0F-4786-9A5F-10D6BE45C3B0");
            ApprovedById = Guid.Parse("BB67A236-EBFC-44D1-964F-A41B869D135B");
        }
    }
}
