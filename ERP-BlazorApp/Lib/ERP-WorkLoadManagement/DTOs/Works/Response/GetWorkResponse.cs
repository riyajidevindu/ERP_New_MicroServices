using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_WorkLoadManagement.DTOs.Works.Response
{
    public class GetWorkResponse
    {
        public Guid WorkId { get; set; }
        public string WorkName { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string WorkCode { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
