using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.GraduateManagement.Core.Entities
{
    public class Graduate : BaseEntity
    {
        public string RegNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string Degree { get; set; }
        public string? CurrentCompany { get; set; }
        public string? CurrentJobPosition { get; set; }

    }
}
