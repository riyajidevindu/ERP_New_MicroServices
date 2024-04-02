using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_GraduateManagement.DTOs.Request
{
    public class CreateGraduateRequest
    {
        public string RegNo { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string? CurrentCompany { get; set; }
        public string? CurrentJobPosition { get; set; }
    }
}
