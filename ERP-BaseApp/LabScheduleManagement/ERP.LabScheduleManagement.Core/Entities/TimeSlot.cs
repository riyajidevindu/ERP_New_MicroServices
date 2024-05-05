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
        public DateTime StartTime { get; set; } 
        public DateTime EndTime { get; set;}
        public double Duration { get; set;}
        public DateTime BookedDate { get; set; } 
        public DateTime? RescheduledDate { get; set; } 
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set; }
    }
}
