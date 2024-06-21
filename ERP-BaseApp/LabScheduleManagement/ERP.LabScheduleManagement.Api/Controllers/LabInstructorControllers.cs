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
    public class LabInstructorControllers : BaseController
    {
        public LabInstructorControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{labInstructorId:guid}")]
        public async Task<IActionResult> GetLabInstructor(Guid labInstructorId)
        {
            var labInstructor = await _unitOfWork.Instructors.GetById(labInstructorId);

            if (labInstructor == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabInstructorByIdResponse>(labInstructor);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLabInstructor([FromBody] CreateLabInstructorRequest labInstructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<LabInstructor>(labInstructor);
            await _unitOfWork.Instructors.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLabInstructor), new { labInstructorId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labInstructorId:guid}")]
        public async Task<IActionResult> UpdateLabInstructor(Guid labInstructorId, [FromBody] UpdateLabInstructorRequest labInstructor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLabInstructor = _mapper.Map<LabInstructor>(labInstructor);
                await _unitOfWork.Instructors.Update(existingLabInstructor);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab instructors.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabInstructors()
        {
            var labInstructors = await _unitOfWork.Instructors.All();

            if (labInstructors == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabInstructorResponse>>(labInstructors);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{labInstructorId:guid}")]
        public async Task<IActionResult> DeleteLabInstructor(Guid labInstructorId)
        {
            var labInstructor = await _unitOfWork.Instructors.GetById(labInstructorId);

            if (labInstructor == null)
            {
                return NotFound();
            }

            await _unitOfWork.Instructors.Delete(labInstructorId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
