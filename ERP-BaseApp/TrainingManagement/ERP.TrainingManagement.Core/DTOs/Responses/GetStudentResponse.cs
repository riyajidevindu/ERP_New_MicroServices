using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetStudentResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RegisterNumber { get; set; }
        public string Department { get; set; }
       
    }
}
