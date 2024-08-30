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
using System.IO.Compression;
using ERP.TrainingManagement.DataServices.Migrations;
using ERP.TrainingManagement.Core.DTOs.Responses;


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

        [HttpPost("upload/cv/{vacancyId:guid}/{studentId:guid}/{firstName}")]
        //[Authorize(Roles = "Student")]
        public async Task<IActionResult> UploadCv([FromForm] UploadFileRequest request, Guid vacancyId,Guid studentId,string firstName)
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var studentId = Guid.Parse(userId);


            using (var memoryStream = new MemoryStream())
            {
                await request.File.CopyToAsync(memoryStream);
                var cvUpload = new CVUpload
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentId,
                    FileName = request.File.FileName,
                    FileData = memoryStream.ToArray(),
                    UploadDate = DateTime.UtcNow,
                    VacancyId = vacancyId,
                    FirstName = firstName


                };

                await _unitOfWork.FileRepository.AddCv(cvUpload);
                await _unitOfWork.CompleteAsync();
            }

            return Ok();
        }

        [HttpGet("download/cvs/{studentId:guid}")]
        //[Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadCvs(Guid studentId)
        {
            var files = await _unitOfWork.FileRepository.GetCvsByStudentIdAsync(studentId);
            if (files == null || !files.Any())
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipEntry.Open())
                        {
                            zipStream.Write(file.FileData, 0, file.FileData.Length);
                        }
                    }
                }

                return File(memoryStream.ToArray(), "application/zip", $"Student_{studentId}_CVs.zip");
            }

        }

        [HttpGet("download/RegistartionLetters/{studentId:guid}")]
        //[Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadsRegsitrationLetters(Guid studentId)
        {
            var files = await _unitOfWork.FileRepository.GetRegistrationLettersByStudentIdAsync(studentId);
            if (files == null || !files.Any())
            {
                return NotFound();
            }

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipEntry = archive.CreateEntry(file.FileName, CompressionLevel.Fastest);
                        using (var zipStream = zipEntry.Open())
                        {
                            zipStream.Write(file.FileData, 0, file.FileData.Length);
                        }
                    }
                }

                return File(memoryStream.ToArray(), "application/zip", $"Student_{studentId}_Regs.zip");
            }

        }

        [HttpPost("upload/registration-letter/{studnetId:guid}")]
        //[Authorize(Roles = "Student")]
        public async Task<IActionResult> UploadRegistrationLetter([FromForm] UploadFileRequest request,Guid studnetId )
        {
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var studentId = Guid.Parse(userId);

            using (var memoryStream = new MemoryStream())
            {
                await request.File.CopyToAsync(memoryStream);
                var registrationLetterUpload = new RegistartionLetterUpload
                {
                    Id = Guid.NewGuid(),
                    StudentId = studnetId,
                    FileName = request.File.FileName,
                    FileData = memoryStream.ToArray(),
                    UploadDate = DateTime.UtcNow
                };

                await _unitOfWork.FileRepository.AddRegistrationLetter(registrationLetterUpload);
                await _unitOfWork.CompleteAsync();
            }

            return Ok();
        }

        [HttpGet("vacancy/{vacancyId:guid}")]
        public async Task<IActionResult> GetCVUploadsByVacancyId(Guid vacancyId)
        {
            var cvUploads = await _unitOfWork.FileRepository.GetCVUploadsByVacancyIdAsync(vacancyId);
            var resultList = new List<GetCVUploadResponse>();

            if (cvUploads == null || !cvUploads.Any())
            {
                return NotFound();
            }

            foreach (var cv in cvUploads)
            {

                // Get the student information using the StudentId
                //var student = await _unitOfWork.studentManagementRepository.GetById(cv.StudentId);
                var student=await _unitOfWork.studentManagementRepository.GetStudentByFirstNameAsync(cv.FirstName);

                if (student != null)
                {
                    var response = new GetCVUploadResponse
                    {
                        Id = cv.Id,
                        StudentId = cv.StudentId,
                        FisrtName = student.FirstName, // Manually adding the FirstName
                        FilePath = cv.FileName,
                        CreatedDate = cv.UploadDate,
                        VacancyId = cv.VacancyId
                    };

                    resultList.Add(response);
                }
                else
                {
                    // Handle the case where the student is not found
                    var response = new GetCVUploadResponse
                    {
                        Id = cv.Id,
                        StudentId = cv.StudentId,
                        FisrtName = "Unknown", // Or some default value
                        FilePath = cv.FileName,
                        CreatedDate = cv.UploadDate,
                        VacancyId = cv.VacancyId
                    };

                    resultList.Add(response);
                }
            }

            return Ok(resultList);
        }

        [HttpGet("download/cv/{cvUploadId:guid}")]
        // [Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadCv(Guid cvUploadId)
        {
            var file = await _unitOfWork.FileRepository.GetCvByUploadIdByCVIDAsync(cvUploadId);
            if (file == null)
            {
                return NotFound();
            }

            var fileName = file.FileName ?? $"CV_{cvUploadId}.pdf";
            return File(file.FileData, "application/octet-stream", fileName);
        }

        [HttpGet("download/Register/{RegisterId:guid}")]
        // [Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> DownloadRegisterLetter(Guid RegisterId)
        {
            var file = await _unitOfWork.FileRepository.GetRegisterIdByRegisterUpload(RegisterId);
            if (file == null)
            {
                return NotFound();
            }

            var fileName = file.FileName ?? $"CV_{RegisterId}.pdf";
            return File(file.FileData, "application/octet-stream", fileName);
        }


        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateApprovalRequest request)
        {
            var approvalRequest = await _unitOfWork.AddApprovalRequestRepository.GetById(id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            approvalRequest.status = 1;
            await _unitOfWork.AddApprovalRequestRepository.Update(approvalRequest);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
        [HttpGet("registration-letters/all")]
        public async Task<IActionResult> GetAllRegistrationLetterUploads()
        {
            var registrationLetters = await _unitOfWork.FileRepository.All();
            if (registrationLetters == null || !registrationLetters.Any())
            {
                return NotFound();
            }

            return Ok(registrationLetters);
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