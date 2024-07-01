using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class UpdateInternshipVacancyRequest
    {
        public Guid Id { get; set; } // The ID of the internship vacancy to be updated
        public string Title { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
    }
}
