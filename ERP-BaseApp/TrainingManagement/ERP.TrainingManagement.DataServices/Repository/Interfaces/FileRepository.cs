using ERP.TrainingManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.TrainingManagement.Core.DTOs;
using ERP.TrainingManagement.DataServices.Data;
using ERP.TrainingManagement.DataServices.Repositories;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.Extensions.Logging;


namespace ERP.TrainingManagement.DataServices.Repository.Interfaces
{
    public class FileRepository : IFileRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<FileRepository> _logger;

        public FileRepository(AppDbContext context) 
        {
            _context = context;
          
        }

        public async Task AddCv(CVUpload cvUpload)
        {
            await _context.CVUploads.AddAsync(cvUpload);
        }

        public async Task AddRegistrationLetter(RegistartionLetterUpload registrationLetterUpload)
        {
            await _context.RegistrationLetterUploads.AddRangeAsync(registrationLetterUpload);
        }

        public async Task<CVUpload> GetCvByStudentId(Guid studentId)
        {
            return await _context.CVUploads.FirstOrDefaultAsync(c => c.StudentId == studentId);
        }

        public async Task<RegistartionLetterUpload> GetRegistrationLetterByStudentId(Guid studentId)
        {
            return await _context.RegistrationLetterUploads.FirstOrDefaultAsync(r => r.StudentId == studentId);
        }
    }
}
