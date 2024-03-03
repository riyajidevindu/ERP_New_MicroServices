using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.Entity
{
    public class Degree : BaseEntity
    {
        public string DegreeName {  get; set; }
        public string? Department {  get; set; }
        public string? Faculty { get; set; }
    }
}
