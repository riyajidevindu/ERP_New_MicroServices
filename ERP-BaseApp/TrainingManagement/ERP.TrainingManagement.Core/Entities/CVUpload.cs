using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.Entities
{
    public class CVUpload : BaseEntity
    {
        [NotMapped]
        public IFormFile File { get; set; }

        public Guid StudentId { get; set; }
        public Student Student { get; set; }
    }
}
