using ERP.TranscriptGenetation.Core.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TranscriptGenetation.Core.Entity
{
    public class Result : BaseEntity
    {
        public string ModuleCode { get; set; }
        public string Type {  get; set; }
        public string ModuleName { get; set; }
        public double Credits { get; set; }
        public string Grade { get; set; }
        public double GPV { get; set; }
        public string Semester { get; set; }

        public Guid StudentId { get; set; }
        public virtual Student Student { get; set; }
    }
}
