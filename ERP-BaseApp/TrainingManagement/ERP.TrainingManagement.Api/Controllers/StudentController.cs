using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Responses;
using ERP.TrainingManagement.DataServices.Repository;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController
    {
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            
        }

        [HttpGet("{registerNumber}")]
        public async Task<IActionResult> GetStudentByRegisterNumber(int registerNumber)
        {
            var student = await _unitOfWork.studentManagementRepository.GetStudentByRegisterNumber(registerNumber);
            if (student == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<GetStudentResponse>(student);
            return Ok(response);
        }
    }
}
