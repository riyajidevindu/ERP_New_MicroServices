using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleRegistration.Core.Entity
{
    public class Student : BaseEntity
    {
        public Student()
        {
            Modules = new HashSet<ModuleOffering>();
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredDate { get; set; }

        public virtual ICollection<ModuleOffering>? Modules { get; set; }

        public Guid? AdvisorId { get; set; }
        public virtual Lecturer Advisor { get; set; }




    }
}
