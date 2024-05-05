using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class LabSpace : BaseEntity
    {
        public LabSpace()
        {
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public string SpaceName { get; set; }
        public string Floor {  get; set; }
        public string TableNumber { get; set; }
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set; }
    }
}
