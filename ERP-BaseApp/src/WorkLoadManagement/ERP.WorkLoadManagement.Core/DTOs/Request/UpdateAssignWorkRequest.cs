using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.WorkLoadManagement.Core.DTOs.Request
{
    public class UpdateAssignWorkRequest
    {
        public Guid AssignedWorkId { get; set; }
        public Guid? WorkId { get; set; }
        public Guid? StaffId { get; set; }
        public string Duration { get; set; } = string.Empty;
        public bool IsRejected { get; set; } = false;
        //public DateTime AssignedDate { get; set; } = DateTime.Now;
        public Guid? AssignByUserId { get; set; }
    }
}
