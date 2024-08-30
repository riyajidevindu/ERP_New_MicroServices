using AutoMapper;
using ERP.GraduateManagement.Core.DTOs.Requests;
using ERP.GraduateManagement.Core.DTOs.Responses;
using ERP.GraduateManagement.Core.Entities;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ERP.GraduateManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraduatesController : BaseController
    {
        public GraduatesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> GetGraduate(Guid graduateId)
        {
            var graduate = await _unitOfWork.GraduateRepo.GetById(graduateId);

            if (graduate == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetGraduateByIdResponse>(graduate);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddGraduate([FromBody] CreateGraduateRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Graduate>(graduate);
            await _unitOfWork.GraduateRepo.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetGraduate), new { graduateId = result.Id }, result);
        }

        [HttpPut]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> UpdateGraduate(Guid graduateId, [FromBody] UpdateGraduateRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingGraduate = _mapper.Map<Graduate>(graduate);
                await _unitOfWork.GraduateRepo.Update(existingGraduate);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the graduate.");
            }

            return NoContent();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllGraduate()
        {
            var graduates = await _unitOfWork.GraduateRepo.All();

            if (graduates == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetGraduateResponse>>(graduates);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> DeleteGraduate(Guid graduateId)
        {
            var graduate = await _unitOfWork.GraduateRepo.GetById(graduateId);

            if (graduate == null)
            {
                return NotFound();
            }

            await _unitOfWork.GraduateRepo.Delete(graduateId);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPost("bulk-add-graduates")]
        public async Task<IActionResult> BulkAddGraduates(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var graduates = new List<CreateGraduateRequest>();

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return BadRequest("No worksheet found.");

                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; rowCount > 1 && row <= rowCount; row++)
                    {
                        var graduate = new CreateGraduateRequest
                        {
                            RegNo = worksheet.Cells[row, 1].Text,
                            FirstName = worksheet.Cells[row, 2].Text,
                            LastName = worksheet.Cells[row, 3].Text,
                            ContactNo = worksheet.Cells[row, 4].Text,
                            Email = worksheet.Cells[row, 5].Text,
                            Degree = worksheet.Cells[row, 6].Text,
                            CurrentCompany = worksheet.Cells[row, 7].Text,
                            CurrentJobPosition = worksheet.Cells[row, 8].Text
                        };

                        graduates.Add(graduate);
                    }
                }
            }

            // Save graduates to the database
            foreach (var graduate in graduates)
            {
                var result = _mapper.Map<Graduate>(graduate);
                await _unitOfWork.GraduateRepo.Add(result);
                await _unitOfWork.CompleteAsync();
            }

            return Ok(new { Message = "GraduateRepo added successfully." });
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportGraduates()
        {
            var graduates = await _unitOfWork.GraduateRepo.All();
            var graduateList = _mapper.Map<List<GetGraduateResponse>>(graduates);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("GraduateRepo");

                // Add headers
                worksheet.Cells[1, 1].Value = "Registration Number";
                worksheet.Cells[1, 2].Value = "Full Name";
                worksheet.Cells[1, 3].Value = "Contact Number";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "Specialization";
                worksheet.Cells[1, 6].Value = "Company";
                worksheet.Cells[1, 7].Value = "Job Position";

                // Add data
                for (int i = 0; i < graduateList.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = graduateList[i].RegNo;
                    worksheet.Cells[i + 2, 2].Value = graduateList[i].FullName;
                    worksheet.Cells[i + 2, 3].Value = graduateList[i].ContactNo;
                    worksheet.Cells[i + 2, 4].Value = graduateList[i].Email;
                    worksheet.Cells[i + 2, 5].Value = graduateList[i].Degree;
                    worksheet.Cells[i + 2, 6].Value = graduateList[i].CurrentCompany;
                    worksheet.Cells[i + 2, 7].Value = graduateList[i].CurrentJobPosition;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Alumni.xlsx";

                return File(content, contentType, fileName);
            }
        }
    }
}
