using AutoMapper;
using ERP.GraduateManagement.Core.DTOs.Requests;
using ERP.GraduateManagement.Core.DTOs.Responses;
using ERP.GraduateManagement.Core.Entities;
using ERP.GraduateManagement.DataServices.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace ERP.GraduateManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GraduatesController : BaseController
    {
        public GraduatesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> GetGraduate(Guid graduateId)
        {
            var graduate = await _unitOfWork.Graduates.GetById(graduateId);

            if (graduate == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetGraduateResponse>(graduate);

            return Ok(result);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddGraduate([FromBody] CreateGraduateRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Graduate>(graduate);
            await _unitOfWork.Graduates.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetGraduate), new { graduateId = result.Id }, result);

        }

        [HttpPut("")]
        public async Task<IActionResult> UpdateGraduate([FromBody] UpdateGraduateRequest graduate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Graduate>(graduate);
            await _unitOfWork.Graduates.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();

        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllGraduate()
        {
            var graduate = await _unitOfWork.Graduates.All();

            if (graduate == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetGraduateResponse>>(graduate);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{graduateId:guid}")]
        public async Task<IActionResult> DeleteGraduate(Guid graduateId)
        {
            var graduate = await _unitOfWork.Graduates.GetById(graduateId);

            if (graduate == null)
            {
                return NotFound();
            }

            await _unitOfWork.Graduates.Delete(graduateId);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }


    }
}

