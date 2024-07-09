using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class CreateCVUploadRequest
    {
        [FromForm(Name = "StudentId")]
        public string StudentId { get; set; }

        public string VacancyId { get; set; }

        [FromForm(Name = "File")]
        public IFormFile File { get; set; }

    }
}
