using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleRegistration.Core.Entity
{
    public class Lecturer : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int EmployeeNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Student>? Advicees { get; set; }
        public virtual ICollection<ModuleOffering>? FirstExaminations { get; set; }
        public virtual ICollection<ModuleOffering>? SecondExaminations { get; set; }
        public virtual ICollection<ModuleOffering>? Moderations { get; set; }
        public virtual ICollection<ModuleOffering>? Coordinations { get; set; }
    }
}
