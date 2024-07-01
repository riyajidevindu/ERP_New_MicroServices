using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class CVUploadSummaryResponse
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; } // Combining FirstName and LastName
       
    }
}
