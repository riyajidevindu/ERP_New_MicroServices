using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetInternshipVacancyResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        //public Guid CoordinatorId { get; set; }
        // public string CoordinatorName { get; set; } 
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
