using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.DTOs.Request
{
    public class UpdateStaffRequest
    {
        public Guid StaffId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeType { get; set; } = string.Empty;
    }
}
