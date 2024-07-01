using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class InternshipVacancySummaryResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Company { get; set; }
        public string CoordinatorName { get; set; } // Combining FirstName and LastName
        public DateTime CreatedDate { get; set; }
    }
}

