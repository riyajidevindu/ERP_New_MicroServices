using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.WorkLoadManagement.DataService.Repositories.Interfaces;
using ERP.WorkLoadManagement.Core.DTOs.Response;
using ERP.WorkLoadManagement.Core.DTOs.Request;
using ERP.WorkLoadManagement.Core.Entities;

namespace ERP.WorkLoadManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignWorkController : BaseController
    {
        public AssignWorkController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
                        
        }

        [HttpGet]
        [Route("{assignWorkId:guid}")]
        public async Task<IActionResult> GetAssignWork(Guid assignWorkId)
        {
            var assignWork = await _unitOfWork.AssignWorks.GetById(assignWorkId);

            if (assignWork == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetAssignWorkByIdResponse>(assignWork);

            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddAssignWork([FromBody] CreateAssignWorkRequest assignWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<AssignWork>(assignWork);
            await _unitOfWork.AssignWorks.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetAssignWork), new { assignWorkId = result.Id }, result);

        }

        [HttpPut]
        [Route("{assignWorkId:guid}")]
        public async Task<IActionResult> UpdateAssignWork(Guid assignWorkId, [FromBody] UpdateAssignWorkRequest assignWork)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingAssignWork = _mapper.Map<AssignWork>(assignWork);
                await _unitOfWork.AssignWorks.Update(existingAssignWork);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the assignWork.");
            }

            return NoContent();

        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllAssignWorks()
        {
            var assignWork = await _unitOfWork.AssignWorks.All();

            if (assignWork == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetAssignWorkResponse>>(assignWork);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{assignWorkId:guid}")]
        public async Task<IActionResult> DeleteAssignWork(Guid assignWorkId)
        {
            var assignWork = await _unitOfWork.AssignWorks.GetById(assignWorkId);

            if (assignWork == null)
            {
                return NotFound();
            }

            await _unitOfWork.AssignWorks.Delete(assignWorkId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }
    }
}
