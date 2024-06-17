using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.DTOs.Response
{
    public class GetWorkByIdResponse
    {
        public Guid WorkId { get; set; }
        public string WorkName { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public string WorkCode { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
    }
}
