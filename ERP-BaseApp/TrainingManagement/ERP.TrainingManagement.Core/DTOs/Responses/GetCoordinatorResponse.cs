using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetCoordinatorResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<InternshipVacancySummary> InternshipVacancies { get; set; }
        public List<ApprovalRequestSummary> ApprovedRequests { get; set; }
    }
    public class InternshipVacancySummary
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
    }

    public class ApprovalRequestSummary
    {
        public Guid Id { get; set; }
        public string StudentName { get; set; }
        public string Company { get; set; }
        public string Status { get; set; }
    }
}
