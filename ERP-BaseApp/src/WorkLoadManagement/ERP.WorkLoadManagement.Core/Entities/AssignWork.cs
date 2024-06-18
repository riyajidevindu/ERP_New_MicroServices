using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.Entities
{
    public class AssignWork : BaseEntity
    {
        public Guid WorkId { get; set; }
        public Guid StaffId { get; set; }
        public string Duration { get; set; }
        public bool IsRejected { get; set; } 
        public DateTime AssignedDate { get; set; }
        public Guid AssignByUserId { get; set; }
    }
}
