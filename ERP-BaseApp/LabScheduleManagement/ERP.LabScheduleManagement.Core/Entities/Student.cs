using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public string RegNo { get; set; }
        public bool present { get; set; }
        public Guid? LabGroupId { get; set; }
        public virtual LabGroup? LabGroup { get; set; }
    }
}
