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
    public class TimeSlotControllers : BaseController
    {
        public TimeSlotControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
            

        }

        [HttpGet]
        [Route("{timeSlotId:guid}")]
        public async Task<IActionResult> GetTimeSlot(Guid timeSlotId)
        {
            var timeSlot = await _unitOfWork.TimeSlots.GetById(timeSlotId);

            if (timeSlot == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetTimeSlotByIdResponse>(timeSlot);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTimeSlot([FromBody] CreateTimeSlotRequest timeSlot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<TimeSlot>(timeSlot);
            await _unitOfWork.TimeSlots.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetTimeSlot), new { timeSlotId = result.Id }, result);

        }

        [HttpPut]
        [Route("{timeSlotId:guid}")]
        public async Task<IActionResult> UpdateTimeSlot(Guid timeSlotId, [FromBody] UpdateTimeSlotRequest timeSlot)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingTimeSlot = _mapper.Map<TimeSlot>(timeSlot);
                await _unitOfWork.TimeSlots.Update(existingTimeSlot);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the timeslot.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllTimeSlots()
        {
            var timeSlots = await _unitOfWork.TimeSlots.All();

            if (timeSlots == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetTimeSlotResponse>>(timeSlots);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{timeSlotId:guid}")]
        public async Task<IActionResult> DeleteTimeSlot(Guid timeSlotId)
        {
            var timeSlot = await _unitOfWork.TimeSlots.GetById(timeSlotId);

            if (timeSlot == null)
            {
                return NotFound();
            }

            await _unitOfWork.TimeSlots.Delete(timeSlotId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

                
        }
    }
}
