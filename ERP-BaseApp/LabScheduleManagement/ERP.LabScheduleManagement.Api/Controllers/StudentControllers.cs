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
    public class StudentControllers : BaseController
    {
        public StudentControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> GetStudent(Guid studentId)
        {
            var student = await _unitOfWork.Students.GetById(studentId);

            if (student == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetStudentByIdResponse>(student);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentRequest student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Student>(student);
            await _unitOfWork.Students.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetStudent), new { studentId = result.Id }, result);

        }


        [HttpPut]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> UpdateStudent(Guid studentId, [FromBody] UpdateStudentRequest student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingStudent = _mapper.Map<Student>(student);
                await _unitOfWork.Students.Update(existingStudent);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the student.");
            }

            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _unitOfWork.Students.All();

            if (students == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetStudentResponse>>(students);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{studentId:guid}")]
        public async Task<IActionResult> DeleteStudent(Guid studentId)
        {
            var student = await _unitOfWork.Students.GetById(studentId);

            if (student == null)
            {
                return NotFound();
            }

            await _unitOfWork.Students.Delete(studentId);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }
    }
}
