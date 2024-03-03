using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.DTO.Request
{
    public class UpdateStudentRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
