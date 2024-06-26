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
    public class LabGroupControllers : BaseController
    {
        public LabGroupControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{labGroupId:guid}")]
        public async Task<IActionResult> GetLabGroup(Guid labGroupId)
        {
            var labGroup = await _unitOfWork.Groups.GetById(labGroupId);

            if (labGroup == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabGroupByIdResponse>(labGroup);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLabGroup([FromBody] CreateLabGroupRequest labGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabGroup>(labGroup);
            await _unitOfWork.Groups.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLabGroup), new { labGroupId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labGroupId:guid}")]
        public async Task<IActionResult> UpdateLabGroup(Guid labGroupId, [FromBody] UpdateLabGroupRequest labGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLabGroup = _mapper.Map<LabGroup>(labGroup);
                await _unitOfWork.Groups.Update(existingLabGroup);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab group.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabGroups()
        {
            var labGroups = await _unitOfWork.Groups.All();

            if (labGroups == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabGroupResponse>>(labGroups);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{labGroupId:guid}")]
        public async Task<IActionResult> DeleteLabGroup(Guid labGroupId)
        {
            var labGroup = await _unitOfWork.Groups.GetById(labGroupId);

            if (labGroup == null)
            {
                return NotFound();
            }

            await _unitOfWork.Groups.Delete(labGroupId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
