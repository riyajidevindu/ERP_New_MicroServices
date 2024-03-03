using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleRegistration.Core.Entity
{
    public class ModuleOffering : BaseEntity
    {
        public Guid ModuleId { get; set; }
        Module Module { get; set; }
        public Guid CoordinatorId { get; set; }
        public Lecturer Coordinator { get; set; }

        public ICollection<Lecturer> FirstExaminers { get; set; }
        public ICollection<Lecturer> SecondExaminers { get; set; }
        public ICollection<Lecturer> Moderators { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
