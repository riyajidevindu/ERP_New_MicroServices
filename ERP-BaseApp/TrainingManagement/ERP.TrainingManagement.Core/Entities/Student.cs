using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class Student : ApplicationUser
    {
       public int Regesiter_Number { get; set; }

        public string Department {  get; set; }
       public ICollection<ApprovalRequest> ApprovalRequests { get; set; }
       public ICollection<CVUpload> CVUploads { get; set; }

        public ICollection<RegistartionLetterUpload> Registers { get; set; }
    }
}

