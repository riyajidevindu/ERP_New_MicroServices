using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class Lab : BaseEntity
    {
        public Lab()
        {
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public string LabName { get; set; }
        public string LabDescription { get; set; }
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set; } = null;
        public Guid? ModuleId { get; set; } = null;
        public virtual Module? Module { get; set; } = null;
    }
}
