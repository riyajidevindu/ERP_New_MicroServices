using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGenetation.Core.Entity
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string RegNo { get; set; }
        public string? DegreeAwarded { get; set; }
        public string? Specialization { get; set; }
        public string? GPA { get; set; }
        public string? AcademicStading { get; set; }


        public virtual ICollection<Result> Results { get; set; }
    }
}
