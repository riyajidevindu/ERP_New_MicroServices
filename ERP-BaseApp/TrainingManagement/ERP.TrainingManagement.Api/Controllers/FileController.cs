using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ERP.TrainingManagement.Core.DTOs.Requests;
using ERP.TrainingManagement.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;


namespace ERP.TrainingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FileController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost("upload/cv")]
       // [Authorize(Roles = "Student")]
        public async Task<IActionResult> UploadCv([FromForm] UploadFileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = Guid.Parse(userId);

            using (var memoryStream = new MemoryStream())
            {
                await request.File.CopyToAsync(memoryStream);
                var cvUpload = new CVUpload
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentId,
                    FileName = request.File.FileName,
                    FileData = memoryStream.ToArray(),
                    UploadDate = DateTime.UtcNow
                };

                await _unitOfWork.FileRepository.AddCv(cvUpload);
                await _unitOfWork.CompleteAsync();
            }

            return Ok();
        }

        [HttpGet("download/cv")]
        //[Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadCv()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = Guid.Parse(userId);

            var cvUpload = await _unitOfWork.FileRepository.GetCvByStudentId(studentId);
            if (cvUpload == null)
            {
                return NotFound();
            }

            return File(cvUpload.FileData, "application/pdf", cvUpload.FileName);
        }

        [HttpPost("upload/registration-letter")]
       // [Authorize(Roles = "Student")]
        public async Task<IActionResult> UploadRegistrationLetter([FromForm] UploadFileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = Guid.Parse(userId);

            using (var memoryStream = new MemoryStream())
            {
                await request.File.CopyToAsync(memoryStream);
                var registrationLetterUpload = new RegistartionLetterUpload
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentId,
                    FileName = request.File.FileName,
                    FileData = memoryStream.ToArray(),
                    UploadDate = DateTime.UtcNow
                };

                await _unitOfWork.FileRepository.AddRegistrationLetter(registrationLetterUpload);
                await _unitOfWork.CompleteAsync();
            }

            return Ok();
        }

        [HttpGet("download/registration-letter")]
      //  [Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadRegistrationLetter()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var studentId = Guid.Parse(userId);

            var registrationLetterUpload = await _unitOfWork.FileRepository.GetRegistrationLetterByStudentId(studentId);
            if (registrationLetterUpload == null)
            {
                return NotFound();
            }

            return File(registrationLetterUpload.FileData, "application/pdf", registrationLetterUpload.FileName);
        }
    }
}

/*4
 * 
 * [Route("api/[controller]")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FileController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("upload/cv")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> UploadCv([FromForm] UploadFileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var studentId = Guid.Parse(userId);

        using (var memoryStream = new MemoryStream())
        {
            await request.File.CopyToAsync(memoryStream);
            var cvUpload = new CVUpload
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                FileName = request.File.FileName,
                FileData = memoryStream.ToArray(),
                UploadDate = DateTime.UtcNow
            };

            await _unitOfWork.Files.AddCv(cvUpload);
            await _unitOfWork.CompleteAsync();
        }

        return Ok();
    }

    [HttpGet("download/cv")]
    [Authorize(Roles = "Student,Coordinator")]
    public async Task<IActionResult> DownloadCv()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var studentId = Guid.Parse(userId);

        var cvUpload = await _unitOfWork.Files.GetCvByStudentId(studentId);
        if (cvUpload == null)
        {
            return NotFound();
        }

        return File(cvUpload.FileData, "application/pdf", cvUpload.FileName);
    }

    [HttpPost("upload/registration-letter")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> UploadRegistrationLetter([FromForm] UploadFileRequest request)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var studentId = Guid.Parse(userId);

        using (var memoryStream = new MemoryStream())
        {
            await request.File.CopyToAsync(memoryStream);
            var registrationLetterUpload = new RegistrationLetterUpload
            {
                Id = Guid.NewGuid(),
                StudentId = studentId,
                FileName = request.File.FileName,
                FileData = memoryStream.ToArray(),
                UploadDate = DateTime.UtcNow
            };

            await _unitOfWork.Files.AddRegistrationLetter(registrationLetterUpload);
            await _unitOfWork.CompleteAsync();
        }

        return Ok();
    }

    [HttpGet("download/registration-letter")]
    [Authorize(Roles = "Student,Coordinator")]
    public async Task<IActionResult> DownloadRegistrationLetter()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var studentId = Guid.Parse(userId);

        var registrationLetterUpload = await _unitOfWork.Files.GetRegistrationLetterByStudentId(studentId);
        if (registrationLetterUpload == null)
        {
            return NotFound();
        }

        return File(registrationLetterUpload.FileData, "application/pdf", registrationLetterUpload.FileName);
    }
}

 */