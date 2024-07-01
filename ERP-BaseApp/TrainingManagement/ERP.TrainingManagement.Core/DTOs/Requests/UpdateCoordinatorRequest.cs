using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class UpdateCoordinatorRequest
    {
        public Guid Id { get; set; } // The ID of the coordinator to be updated
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
