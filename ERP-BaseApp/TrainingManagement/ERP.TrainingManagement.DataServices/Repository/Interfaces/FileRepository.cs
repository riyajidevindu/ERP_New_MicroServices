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

        public async Task<IEnumerable<CVUpload>> GetCvsByStudentIdAsync(Guid studentId)
        {
            return await _context.CVUploads.Where(c => c.StudentId == studentId).ToListAsync();
        }

        public async Task<List<CVUpload>> GetCVUploadsByVacancyIdAsync(Guid vacancyId)
        {
            return await _context.CVUploads
                .Where(c => c.VacancyId == vacancyId)
                .ToListAsync();
        }

        public async Task<IEnumerable<RegistartionLetterUpload>> GetRegistrationLettersByStudentIdAsync(Guid studentId)
        {
            return await _context.RegistrationLetterUploads.Where(r => r.StudentId == studentId).ToListAsync();
        }

    }
}
