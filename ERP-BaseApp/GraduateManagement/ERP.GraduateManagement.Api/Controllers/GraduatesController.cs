using AutoMapper;
using ERP.GraduateManagement.Core.DTOs.Requests;
using ERP.GraduateManagement.Core.DTOs.Responses;
using ERP.GraduateManagement.Core.Entities;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using OfficeOpenXml;

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
            var graduate = await _unitOfWork.Graduates.GetById(graduateId);

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
            await _unitOfWork.Graduates.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetGraduate), new { graduateId = result.Id }, result);

        }

        [HttpPut]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> UpdateGraduate(Guid graduateId,[FromBody] UpdateGraduateRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } 

            try
            {
                var existingGraduate = _mapper.Map<Graduate>(graduate);
                await _unitOfWork.Graduates.Update(existingGraduate);
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
            var graduate = await _unitOfWork.Graduates.All();

            if (graduate == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetGraduateResponse>>(graduate);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> DeleteGraduate(Guid graduateId)
        {
            var graduate = await _unitOfWork.Graduates.GetById(graduateId);

            if (graduate == null)
            {
                return NotFound();
            }

            await _unitOfWork.Graduates.Delete(graduateId);
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

                    for (int row = 2; row <= rowCount; row++)
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
                await _unitOfWork.Graduates.Add(result);
                await _unitOfWork.CompleteAsync();
            }

            return Ok(new { Message = "Graduates added successfully." });
        }



    }
}

