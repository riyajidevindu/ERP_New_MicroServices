using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.WorkLoadManagement.DataService.Repositories.Interfaces;
using ERP.WorkLoadManagement.Core.DTOs.Request;
using ERP.WorkLoadManagement.Core.DTOs.Response;
using ERP.WorkLoadManagement.Core.Entities;

namespace ERP.WorkLoadManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : BaseController
    {
        public StaffController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{staffId:guid}")]
        public async Task<IActionResult> GetStaff(Guid staffId)
        {
            var staff = await _unitOfWork.Staffs.GetById(staffId);

            if (staff == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetStaffByIdResponse>(staff);

            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddStaff([FromBody] CreateStaffRequest staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Staff>(staff);
            await _unitOfWork.Staffs.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetStaff), new { staffId = result.Id }, result);

        }

        [HttpPut]
        [Route("{staffId:guid}")]
        public async Task<IActionResult> UpdateStaff(Guid staffId, [FromBody] UpdateStaffRequest staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingStaff = _mapper.Map<Staff>(staff);
                await _unitOfWork.Staffs.Update(existingStaff);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the staff.");
            }

            return NoContent();

        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllStaffs()
        {
            var staff = await _unitOfWork.Staffs.All();

            if (staff == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetStaffResponse>>(staff);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{staffId:guid}")]
        public async Task<IActionResult> DeleteStaff(Guid staffId)
        {
            var staff = await _unitOfWork.Staffs.GetById(staffId);

            if (staff == null)
            {
                return NotFound();
            }

            await _unitOfWork.Staffs.Delete(staffId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
