using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class ScheduledLab : BaseEntity
    {
        public ScheduledLab() 
        {
            LabEquipmets = new HashSet<LabEquipment>();
            LabGroups = new HashSet<LabGroup>();    
        }
        public bool Resheduled { get; set; }
        public bool Completed { get; set; }

        public Guid? LabInstructorId { get; set; }
        public Guid? LabCoordinatorId { get; set; }
        public Guid? LabId { get; set; }
        public Guid? TimeSlotId { get; set; }
        public Guid? LabSpaceId { get; set; }
        public virtual ICollection<LabEquipment>? LabEquipmets { get; set; }
        public virtual ICollection<LabGroup>? LabGroups { get; set; }
        public virtual LabInstructor? LabInstructor { get; set; }
        public virtual LabCoordinator? LabCoordinator { get; set; }
        public virtual Lab? Lab {  get; set; } 
        public virtual TimeSlot? TimeSlot { get; set; }
        public virtual LabSpace? LabSpace { get; set; }

    }
}
