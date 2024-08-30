using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ERP.TrainingManagement.Core.Entities
{
        public class ApplicationUser : IdentityUser<Guid>
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
             
            public string Email {  get; set; }
        }

        
    }

