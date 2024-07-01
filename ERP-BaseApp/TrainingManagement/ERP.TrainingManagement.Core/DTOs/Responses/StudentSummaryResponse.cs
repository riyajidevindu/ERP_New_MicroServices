using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class StudentSummaryResponse
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } // Combining FirstName and LastName
        public string Email { get; set; }
        public int RegisterNumber { get; set; }
        public string Department { get; set; }
    }
}
