using ERP.LabEquipmentManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.DTOs.Responses;
using ERP.LabEquipmentManagement.Api.Services.Interfaces;
using OfficeOpenXml;

namespace ERP.LabEquipmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabEquipmentController :BaseController
    {
        private readonly ILabEquipmentNotificationPublisherService _labEquipmentNotification;
        public LabEquipmentController(
            IUnitOfWork unitOfWork, 
            IMapper mapper,
            ILabEquipmentNotificationPublisherService labEquipmentNotification) : base(unitOfWork, mapper)
        {
            _labEquipmentNotification = labEquipmentNotification;
        }

        [HttpGet]
        [Route("{labEquipmentId:guid}")]
        public async Task<IActionResult> GetLabEquipment(Guid labEquipmentId)
        {
            var labEquipment = await _unitOfWork.LabEquipments.GetById(labEquipmentId);

            if (labEquipment == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabEquipmentResponse>(labEquipment);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLabEquipment([FromBody] CreateLabEquipmentRequest labEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabEquipment>(labEquipment);
            await _unitOfWork.LabEquipments.Add(result);
            await _unitOfWork.CompleteAsync();

            //await _labEquipmentNotification.SentNotification(result.Id, result.EquipmentName);

            return CreatedAtAction(nameof(GetLabEquipment), new { labEquipmentId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labEquipmentId:guid}")]
        public async Task<IActionResult> UpdateLabEquipment(Guid labEquipmentId, [FromBody] UpdateLabEquipmentRequest labEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try 
            {
                var result = _mapper.Map<LabEquipment>(labEquipment);
                await _unitOfWork.LabEquipments.Update(result);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab equipment.");
            }



            return NoContent();

        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabEquipment()
        {
            var labEquipment = await _unitOfWork.LabEquipments.All();

            if (labEquipment == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabEquipmentResponse>>(labEquipment);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{labEquipmentId:guid}")]
        public async Task<IActionResult> DeleteLabEquipment(Guid labEquipmentId)
        {
            var labEquipment = await _unitOfWork.LabEquipments.GetById(labEquipmentId);

            if (labEquipment == null)
            {
                return NotFound();
            }

            await _unitOfWork.LabEquipments.Delete(labEquipmentId);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }

        [HttpPost("bulk-add-labEquipment")]
        public async Task<IActionResult> BulkAddGraduates(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var labEquipments = new List<CreateLabEquipmentRequest>();

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
                        var labEquipment = new CreateLabEquipmentRequest
                        {
                            EquipmentRegisterId = worksheet.Cells[row, 2].Text,
                            EquipmentName = worksheet.Cells[row, 3].Text,
                            Location = worksheet.Cells[row, 1].Text,
                            SelectCategory = worksheet.Cells[row, 5].Text,
                            IsActive = bool.Parse(worksheet.Cells[row, 4].Text),
                            Price = double.Parse(worksheet.Cells[row, 6].Text),
                            Description = worksheet.Cells[row, 7].Text,
                            PurchasedDate = DateTime.Parse(worksheet.Cells[row, 8].Text)


                        };

                        labEquipments.Add(labEquipment);
                    }
                }
            }

            // Save labEquipments to the database
            foreach (var labEquipment in labEquipments)
            {
                var result = _mapper.Map<LabEquipment>(labEquipment);
                await _unitOfWork.LabEquipments.Add(result);
                await _unitOfWork.CompleteAsync();
            }

            return Ok(new { Message = "Lab Equipments added successfully." });
        }

        [HttpGet("export")]
        public async Task<IActionResult> ExportLabEquipments()
        {
            var labEquipments = await _unitOfWork.LabEquipments.All();
            var labEquipmentList = _mapper.Map<List<GetLabEquipmentResponse>>(labEquipments);

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("LabEquipments");

                // Add headers
                worksheet.Cells[1, 1].Value = "EquipmentRegisterId";
                worksheet.Cells[1, 2].Value = "EquipmentName";
                worksheet.Cells[1, 3].Value = "SelectCategory";
                worksheet.Cells[1, 4].Value = "Price";
                worksheet.Cells[1, 5].Value = "Location";
                worksheet.Cells[1, 6].Value = "IsActive";
                worksheet.Cells[1, 7].Value = "Description";
                worksheet.Cells[1, 8].Value = "PurchasedDate";

                // Add data
                for (int i = 0; i < labEquipmentList.Count; i++)
                {
                    worksheet.Cells[i + 2, 1].Value = labEquipmentList[i].EquipmentRegisterId;
                    worksheet.Cells[i + 2, 2].Value = labEquipmentList[i].EquipmentName;
                    worksheet.Cells[i + 2, 3].Value = labEquipmentList[i].SelectCategory;
                    worksheet.Cells[i + 2, 4].Value = labEquipmentList[i].Price;
                    worksheet.Cells[i + 2, 5].Value = labEquipmentList[i].Location;
                    worksheet.Cells[i + 2, 6].Value = labEquipmentList[i].IsActive;
                    worksheet.Cells[i + 2, 7].Value = labEquipmentList[i].Description;
                    worksheet.Cells[i + 2, 8].Value = labEquipmentList[i].PurchasedDate;
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                var content = stream.ToArray();
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "Lab_Equipments.xlsx";

                return File(content, contentType, fileName);
            }
        }


    }
}


