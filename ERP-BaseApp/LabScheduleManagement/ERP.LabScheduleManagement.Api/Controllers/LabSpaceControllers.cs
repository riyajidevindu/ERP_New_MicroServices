using AutoMapper;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class LabSpaceControllers : BaseController
    {
        public LabSpaceControllers(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{labSpaceId:guid}")]
        public async Task<IActionResult> GetLabSpace(Guid labSpaceId)
        {
            var labSpace = await _unitOfWork.Spaces.GetById(labSpaceId);

            if (labSpace == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabSpaceByIdResponse>(labSpace);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLabSpace([FromBody] CreateLabSpaceRequest labSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabSpace>(labSpace);
            await _unitOfWork.Spaces.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLabSpace), new { labSpaceId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labSpaceId:guid}")]
        public async Task<IActionResult> UpdateLabSpace(Guid labSpaceId, [FromBody] UpdateLabSpaceRequest labSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLabSpace = _mapper.Map<LabSpace>(labSpace);
                await _unitOfWork.Spaces.Update(existingLabSpace);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab spaces.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabSpaces()
        {
            var labSpaces = await _unitOfWork.Spaces.All();

            if (labSpaces == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabSpaceResponse>>(labSpaces);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{labSpaceId:guid}")]
        public async Task<IActionResult> DeleteLabSpace(Guid labSpaceId)
        {
            var labSpace = await _unitOfWork.Spaces.GetById(labSpaceId);

            if (labSpace == null)
            {
                return NotFound();
            }

            await _unitOfWork.Spaces.Delete(labSpaceId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
