using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.ModuleRegistration.Core.Entity
{
    public class ModuleOfferingModerator
    {
        public Guid ModuleOfferingId { get; set; }
        public ModuleOffering ModuleOffering { get; set; }

        public Guid LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }
    }
}
