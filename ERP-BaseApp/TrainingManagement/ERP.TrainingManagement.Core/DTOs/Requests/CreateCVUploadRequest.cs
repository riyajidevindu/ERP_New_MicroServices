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
        public Guid StudentId { get; set; }

        public Guid VacancyId { get; set; }

        [FromForm(Name = "File")]
        public IFormFile File { get; set; }

        public string FirstName {  get; set; }

        public CreateCVUploadRequest()
        {
            StudentId= Guid.Parse("1EA0E241-DC0F-4786-9A5F-10D6BE45C3B0");
            
        }

    }
}
