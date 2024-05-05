using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class LabInstructor : BaseEntity
    {
        public LabInstructor() 
        {
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public string InstructorName { get; set; }
        public string EmailAddress { get; set; }
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set; }
    }
}
