using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.ModuleRegistration.Core.Types;

namespace ERP.ModuleRegistration.Core.Entity
{
    public class Module : BaseEntity
    {
        public string ModuleName { get; set; } = string.Empty;
        public string ModuleCode { get; set; } = string.Empty;
        public int Credits { get; set; }
        public DepartmentType Department { get; set; }
        public string Curriculum { get; set; }
    }
}
