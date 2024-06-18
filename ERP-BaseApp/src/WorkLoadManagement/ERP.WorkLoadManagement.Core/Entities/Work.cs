using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.Entities
{
    public class Work : BaseEntity
    {
        public string WorkName { get; set; } 
        public string WorkType { get; set; } 
        public string WorkCode { get; set; } 
        public string? Description { get; set; }

        // Navigation property for the related AssignWork entities
        public virtual ICollection<AssignWork> AssignWorks { get; set; } = new List<AssignWork>();

    }
}
