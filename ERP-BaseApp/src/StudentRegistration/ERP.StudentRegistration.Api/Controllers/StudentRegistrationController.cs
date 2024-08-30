using AutoMapper;

using ERP.Messaging.Core.Service.Intefeaces;
using ERP.Messaging.Core.Student;
using ERP.StudentRegistration.Core.DTO.Request;
using ERP.StudentRegistration.Core.DTOs.Response;
using ERP.StudentRegistration.Core.Entity;
using ERP.StudentRegistration.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.StudentRegistration.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentRegistrationController : BaseController
{
    public readonly IStudentCreatedNotificationPublisherService _studentService;
    public StudentRegistrationController(IUnitOfWork unitOfWork, 
                                          IMapper mapper,
                                          IStudentCreatedNotificationPublisherService studentService
                                          ) : base(unitOfWork, mapper)
    {
        _studentService = studentService;
    }

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetStudent(Guid studentId)
    {
        var student = await _unitOfWork.Students.GetAsync(studentId);
        if (student == null)
        {
            return NotFound();
        }
        var results = _mapper.Map<StudentResponse>(student);
        return Ok(results);
    }


    [HttpGet]
  
    public async Task<IActionResult> GetAllStudent()
    {
        var students = await _unitOfWork.Students.GetAllAsync();
        var results = _mapper.Map<IEnumerable<StudentResponse>>(students);
        return Ok(results);
    }

    [HttpDelete]
    [Route("{studentId:guid}")]
    public async Task<IActionResult> DeleteStudent(Guid studentId)
    {
        var driver = await _unitOfWork.Students.GetAsync(studentId);

        if (driver == null)
        {
            return NotFound();
        }

        await _unitOfWork.Students.DeleteAsync(studentId);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }


    [HttpPost("")]
    public async Task<IActionResult> AddStudent([FromBody] CreateStudentRequest student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = _mapper.Map<Student>(student);
        await _unitOfWork.Students.AddAsync(result);
        await _unitOfWork.CompleteAsync();

        StudentCreatedNotificationRecord studentRecord = new StudentCreatedNotificationRecord
        (StudentId: result.Id,
            RegistrationNumber: result.RegistrationNumber,
            StudentFullName: $"{result.FirstName} {result.LastName}",
            DateOfBirth: result.DateOfBirth
        );

        await _studentService.SentNotification(studentRecord);

        return Ok(student);
    }

    [HttpPost("SentNotification")]
    public async Task<IActionResult> SentNotifation([FromBody] CreateStudentRequest student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = _mapper.Map<Student>(student);
       

        StudentCreatedNotificationRecord studentRecord = new StudentCreatedNotificationRecord
        (StudentId: result.Id,
            RegistrationNumber: result.RegistrationNumber,
            StudentFullName: $"{result.FirstName} {result.LastName}",
            DateOfBirth: result.DateOfBirth
        );

        await _studentService.SentNotification(studentRecord);

        return Ok(student);
    }



    [HttpPut("")]
    public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest student)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }

        var result = _mapper.Map<Student>(student);
        await _unitOfWork.Students.UpdateAsync(result);
        await _unitOfWork.CompleteAsync();
        return NoContent();
    }

}


