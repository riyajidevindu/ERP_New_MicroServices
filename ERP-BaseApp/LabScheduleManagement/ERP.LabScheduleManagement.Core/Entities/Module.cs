using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.LabScheduleManagement.Core.Entities
{
    public class Module : BaseEntity
    {
        public Module() 
        {
            Labs = new HashSet<Lab>();
        }
        public string ModuleName { get; set; }
        public string ModuleCoordinator {  get; set; }
        public int NoofLabs { get; set; }

        public virtual ICollection<Lab>? Labs { get; set; }
    }
}
