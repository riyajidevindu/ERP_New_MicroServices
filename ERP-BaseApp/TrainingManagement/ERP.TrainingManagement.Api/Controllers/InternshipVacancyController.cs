using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Requests;
using ERP.TrainingManagement.Core.DTOs.Responses;
using ERP.TrainingManagement.Core.Entities;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure.Core;

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

        [HttpPut]
        [Route("{VacancyId:guid}")]
        public async Task<IActionResult> UpdateInternVacancy(Guid VacancyId, [FromBody] UpdateInternshipVacancyRequest Vacancy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingVacancyRequest = await _unitOfWork.AddJobRepository.GetById(VacancyId);
            if (existingVacancyRequest == null)
            {
                return NotFound();
            }

            _mapper.Map(Vacancy,existingVacancyRequest);
            existingVacancyRequest.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.AddJobRepository.Update(existingVacancyRequest);
            await _unitOfWork.CompleteAsync();

            return NoContent();


            
        }

        [HttpDelete]
        [Route("{VacancyId:guid}")]
        public async Task<IActionResult> DeleteGraduate(Guid VacancyId)
        {
            var vacancy = await _unitOfWork.AddJobRepository.GetById(VacancyId);

            if (vacancy == null)
            {
                return NotFound();
            }

            await _unitOfWork.AddJobRepository.Delete(VacancyId);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllVacancys()
        {
            var vacancies = await _unitOfWork.AddJobRepository.All();

            if (vacancies == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetInternshipVacancyResponse>>(vacancies);

            return Ok(result);
        }

        

    }
}
