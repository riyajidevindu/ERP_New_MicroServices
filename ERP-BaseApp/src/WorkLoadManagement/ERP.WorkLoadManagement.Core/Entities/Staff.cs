using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.Entities
{
    public class Staff : BaseEntity
    {
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; } 
        public string EmployeeType { get; set; }

        // Navigation property for the related AssignWork entities
        public virtual ICollection<AssignWork> AssignWorks { get; set; } = new List<AssignWork>();
    }
}
