using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_StudentRegistration.DTOs.Response
{
    public class StudentResponse
    {
        public Guid StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
