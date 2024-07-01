using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class UpdateStudentRequest
    {
        public Guid Id { get; set; } // The ID of the student to be updated
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RegisterNumber { get; set; }
        public string Department { get; set; }
    }
}
