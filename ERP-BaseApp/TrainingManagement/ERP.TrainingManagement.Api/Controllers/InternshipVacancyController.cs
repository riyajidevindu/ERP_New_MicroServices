using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Requests;
using ERP.TrainingManagement.Core.DTOs.Responses;
using ERP.TrainingManagement.Core.Entities;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternshipVacancyController : BaseController
    {
        public InternshipVacancyController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet("{VacancyId:guid}")]
        public async Task<IActionResult> GetVacancy(Guid VacancyId)
        {
            var vacancy = await _unitOfWork.AddJobRepository.GetById(VacancyId);

            if (vacancy == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetInternshipVacancyResponse>(vacancy);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddInternshipVacancy([FromBody] CreateInternshipVacancyRequest internshipVacancyRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<InternshipVacancy>(internshipVacancyRequest);
            await _unitOfWork.AddJobRepository.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetVacancy), new { VacancyId = result.Id }, result);
        }
    }
}
