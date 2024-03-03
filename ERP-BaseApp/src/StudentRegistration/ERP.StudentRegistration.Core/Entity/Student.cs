using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.StudentRegistration.Core.Entity
{
    public class Student : BaseEntity
    {
        
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? RegistrationNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public DateTime RegisteredDate { get; set; }

        public Guid? SemesterId { get; set; }
        [ForeignKey("SemesterId")]
        public virtual Semester Semester { get; set; }

        public Guid? DegreeId { get; set; }
        [ForeignKey("DegreeId")]
        public virtual Degree Degree { get; set; }

        public Guid? AdvisorId { get; set; }
        [ForeignKey("AdvisorId")]
        public virtual Lecturer Advisor { get; set; }

    }
}
