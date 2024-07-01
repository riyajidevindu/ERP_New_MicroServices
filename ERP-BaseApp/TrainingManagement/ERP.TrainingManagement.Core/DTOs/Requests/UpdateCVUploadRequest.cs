using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Core.DTOs.Requests
{
    public class UpdateCVUploadRequest
    {
        public Guid Id { get; set; } // The ID of the CV upload to be updated
        public IFormFile File { get; set; } // New file to replace the existing one
    }
}
