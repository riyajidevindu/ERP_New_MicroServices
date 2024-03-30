using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_GraduateManagement.DTOs.Response
{
    public class GetGraduateResponse
    {
        public Guid GraduateId { get; set; }
        public string RegNo { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Degree { get; set; } = string.Empty;
        public string? CurrentCompany { get; set; }
        public string? CurrentJobPosition { get; set; }
    }
}
