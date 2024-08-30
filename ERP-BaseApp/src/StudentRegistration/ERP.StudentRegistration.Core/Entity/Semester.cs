using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.Entity
{
    public class Semester : BaseEntity
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? AcademicYear { get; set; }
        public string? Department { get; set; } = string.Empty;

        public Guid? DegreeId {  get; set; }
        [ForeignKey("DegreeId")]
        public virtual Degree Degree { get; set; }
    }
}
