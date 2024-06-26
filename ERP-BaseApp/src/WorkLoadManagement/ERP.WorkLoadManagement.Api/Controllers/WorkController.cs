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
    public class WorkController : BaseController
    {
        public WorkController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {            
        }

        [HttpGet]
        [Route("{workId:guid}")]
        public async Task<IActionResult> GetWork(Guid workId)
        {
            var work = await _unitOfWork.Works.GetById(workId);

            if (work == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetWorkByIdResponse>(work);

            return Ok(result);
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddWork([FromBody] CreateWorkRequest work)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Work>(work);
            await _unitOfWork.Works.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetWork), new { workId = result.Id }, result);

        }

        [HttpPut]
        [Route("{workId:guid}")]
        public async Task<IActionResult> UpdateWork(Guid workId, [FromBody] UpdateWorkRequest work)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var existingWork = _mapper.Map<Work>(work);
                await _unitOfWork.Works.Update(existingWork);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the work.");
            }

            return NoContent();

        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllWorks()
        {
            var work = await _unitOfWork.Works.All();

            if (work == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetWorkResponse>>(work);

            return Ok(result);
        }


        [HttpDelete]
        [Route("{workId:guid}")]
        public async Task<IActionResult> DeleteWork(Guid workId)
        {
            var work = await _unitOfWork.Works.GetById(workId);

            if (work == null)
            {
                return NotFound();
            }

            await _unitOfWork.Works.Delete(workId);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }




    }
}
