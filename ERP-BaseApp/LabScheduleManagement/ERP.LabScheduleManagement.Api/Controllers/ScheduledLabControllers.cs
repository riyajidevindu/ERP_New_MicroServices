using AutoMapper;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Runtime.CompilerServices;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class ScheduledLabControllers : BaseController
    {
        public ScheduledLabControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{scheduledLabId:guid}")]
        public async Task<IActionResult> GetScheduledLab(Guid scheduledLabId)
        {
            var scheduledLab = await _unitOfWork.ScheduledLabs.GetById(scheduledLabId);

            if (scheduledLab == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetScheduledLabByIdResponse>(scheduledLab);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddScheduledLab([FromBody] CreateScheduledLabRequest scheduledLab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<ScheduledLab>(scheduledLab);
            await _unitOfWork.ScheduledLabs.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetScheduledLab), new { scheduledLabId = result.Id }, result);

        }


        [HttpPut]
        [Route("{scheduledLabId:guid}")]
        public async Task<IActionResult> UpdateScheduledLab(Guid scheduledLabId, [FromBody] UpdateScheduledLabRequest scheduledLab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingScheduledLab = _mapper.Map<ScheduledLab>(scheduledLab);
                await _unitOfWork.ScheduledLabs.Update(existingScheduledLab);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab schedule.");
            }

            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllScheduledLab()
        {
            var scheduledLab = await _unitOfWork.ScheduledLabs.All();

            if (scheduledLab == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetScheduledLabResponse>>(scheduledLab);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{scheduledLabId:guid}")]
        public async Task<IActionResult> DeleteScheduledLab(Guid scheduledLabId)
        {
            var scheduledLab = await _unitOfWork.ScheduledLabs.GetById(scheduledLabId);

            if (scheduledLab == null)
            {
                return NotFound();
            }

            await _unitOfWork.ScheduledLabs.Delete(scheduledLabId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }

    }
}
