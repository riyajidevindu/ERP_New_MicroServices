using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class CreateCVUploadRequest
    {
        public IFormFile File { get; set; }
        public Guid StudentId { get; set; }
    }
}
