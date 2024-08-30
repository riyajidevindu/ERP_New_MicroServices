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
    public class LabCoordinatorControllers : BaseController
    {
        public LabCoordinatorControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{labCoordinatorId:guid}")]
        public async Task<IActionResult> GetLabCoordinator(Guid labCoordinatorId)
        {
            var labCoordinator = await _unitOfWork.Coordinators.GetById(labCoordinatorId);

            if (labCoordinator == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabCoordinatorByIdResponse>(labCoordinator);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLabCoordinator([FromBody] CreateLabCoordinatorRequest labCoordinator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabCoordinator>(labCoordinator);
            await _unitOfWork.Coordinators.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLabCoordinator), new { labCoordinatorId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labCoordinatorId:guid}")]
        public async Task<IActionResult> UpdateLabCoordinator(Guid labCoordinatorId, [FromBody] UpdateLabCoordinatorRequest labCoordinator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLabCoordinator = _mapper.Map<LabCoordinator>(labCoordinator);
                await _unitOfWork.Coordinators.Update(existingLabCoordinator);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab coordinators.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabCoordinators()
        {
            var labCoordinators = await _unitOfWork.Coordinators.All();

            if (labCoordinators == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabCoordinatorResponse>>(labCoordinators);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{labCoordinatorId:guid}")]
        public async Task<IActionResult> DeleteLabCoordinator(Guid labCoordinatorId)
        {
            var labCoordinator = await _unitOfWork.Coordinators.GetById(labCoordinatorId);

            if (labCoordinator == null)
            {
                return NotFound();
            }

            await _unitOfWork.Coordinators.Delete(labCoordinatorId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
