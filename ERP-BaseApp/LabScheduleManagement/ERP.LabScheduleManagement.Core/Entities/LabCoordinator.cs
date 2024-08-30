using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class LabCoordinator : BaseEntity
    {
        public LabCoordinator()
        {
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public string CoordinatorName { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set; }
    }
}
