using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class LabGroup : BaseEntity
    {
        public LabGroup() 
        {
            Students = new HashSet<Student>();
            ScheduledLabs = new HashSet<ScheduledLab>();
        }
        public string GroupNumber { get; set; }
        public int Batch {  get; set; }
        public string Specilization { get;set; }
        public int NoOfStudents { get; set; } 
        public virtual ICollection<Student>? Students { get; set; }
        public virtual ICollection<ScheduledLab>? ScheduledLabs { get; set;}
    }
}
