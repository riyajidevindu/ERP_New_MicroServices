using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_WorkLoadManagement.DTOs.Staffs.Response
{
    public class GetStaffResponse
    {
        public Guid StaffId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeId { get; set; } = string.Empty;
        public string EmployeeType { get; set; } = string.Empty;
    }
}
