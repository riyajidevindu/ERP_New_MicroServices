using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class Student : ApplicationUser
    {
         
       public ICollection<ApprovalRequest> ApprovalRequests { get; set; }
       public ICollection<CVUpload> CVUploads { get; set; }
    }
}

