using ERP.LabEquipmentManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;
using ERP.LabEquipmentManagement.Core.DTOs.Requests;
using ERP.LabEquipmentManagement.Core.DTOs.Responses;

namespace ERP.LabEquipmentManagement.Api.Controllers
{
    public class LabEquipmentController :BaseController
    {
        public LabEquipmentController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
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
        public async Task<IActionResult> AddGraduate([FromBody] CreateLabEquipmentRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabEquipment>(graduate);
            await _unitOfWork.LabEquipments.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLabEquipment), new { labEquipmentId = result.Id }, result);

        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateLabWeuipment([FromBody] UpdateLabEquipmentRequest labEquipment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabEquipment>(labEquipment);
            await _unitOfWork.LabEquipments.Update(result);
            await _unitOfWork.CompleteAsync();

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


