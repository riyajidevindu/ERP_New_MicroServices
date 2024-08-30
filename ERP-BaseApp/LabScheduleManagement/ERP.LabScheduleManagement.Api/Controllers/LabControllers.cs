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
    public class LabControllers : BaseController
    {
        public LabControllers(
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{labId:guid}")]
        public async Task<IActionResult> GetLab(Guid labId)
        {
            var lab = await _unitOfWork.Labs.GetById(labId);

            if (lab == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetLabByIdResponse>(lab);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLab([FromBody] CreateLabRequest lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Lab>(lab);
            await _unitOfWork.Labs.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetLab), new { labId = result.Id }, result);

        }

        [HttpPut]
        [Route("{labId:guid}")]
        public async Task<IActionResult> UpdateLab(Guid labId, [FromBody] UpdateLabRequest lab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingLab = _mapper.Map<Lab>(lab);
                await _unitOfWork.Labs.Update(existingLab);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the lab.");
            }


            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllLabs()
        {
            var labs = await _unitOfWork.Labs.All();

            if (labs == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetLabResponse>>(labs);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{labId:guid}")]
        public async Task<IActionResult> DeleteLab(Guid labId)
        {
            var lab = await _unitOfWork.Labs.GetById(labId);

            if (lab == null)
            {
                return NotFound();
            }

            await _unitOfWork.Labs.Delete(labId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
