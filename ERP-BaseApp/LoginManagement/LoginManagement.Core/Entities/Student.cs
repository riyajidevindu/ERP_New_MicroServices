using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginManagement.Core.Entities
{
    public class Student : ApplicationUser
    {
        public int Regesiter_Number { get; set; }

        public string Department { get; set; }

    }
}
