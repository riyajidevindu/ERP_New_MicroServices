using ERP.LabEquipmentManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.DTOs.Responses;
using ERP.LabEquipmentManagement.Api.Services.Interfaces;

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

        [HttpPost("")]
        public async Task<IActionResult> AddLabEquipment([FromBody] CreateLabEquipmentRequest labEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabEquipment>(labEquipment);
            await _unitOfWork.LabEquipments.Add(result);
            await _unitOfWork.CompleteAsync();

            await _labEquipmentNotification.SentNotification(result.Id, result.EquipmentName);

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


        [HttpGet("")]
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


    }
}


