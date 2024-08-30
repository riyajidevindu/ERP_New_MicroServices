using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ERP.TrainingManagement.Core.DTOs.Responses
{
    public class GetCVUploadResponse
    {
        public Guid Id { get; set; }
        public string FilePath { get; set; }

        public Guid VacancyId { get; set; }
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        
        public string FisrtName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
