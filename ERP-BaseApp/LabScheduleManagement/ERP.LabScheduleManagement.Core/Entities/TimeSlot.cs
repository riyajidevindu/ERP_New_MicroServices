using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class TimeSlot : BaseEntity
    {
        public TimeSlot()
        {
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set;}
        public TimeOnly Duration { get; set;}
        public DateOnly BookedDate { get; set; }
        public DateOnly RescheduledDate { get; set; }
        public virtual ICollection<ScheduledLab> ScheduledLabs { get; set; }
    }
}
