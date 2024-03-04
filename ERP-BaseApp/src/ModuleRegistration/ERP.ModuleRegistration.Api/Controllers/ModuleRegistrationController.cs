using AutoMapper;
using ERP.ModuleRegistration.Api.Controllers;
using ERP.ModuleRegistration.Core.DTOs.Request;
using ERP.ModuleRegistration.Core.DTOs.Response;
using ERP.ModuleRegistration.Core.Entity;
using ERP.ModuleRegistration.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.StudentRegistration.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ModuleRegistrationController : BaseController
{
    public ModuleRegistrationController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> GetStudent(Guid driverId)
    {
        var driver = await _unitOfWork.Students.GetAsync(driverId);
        if (driver == null)
        {
            return NotFound();
        }
        var results = _mapper.Map<StudentResponse>(driver);
        return Ok(results);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllStudent()
    {
        var drivers = await _unitOfWork.Students.GetAllAsync();
        var results = _mapper.Map<IEnumerable<StudentResponse>>(drivers);
        return Ok(results);
    }

    [HttpDelete]
    [Route("{driverId:guid}")]
    public async Task<IActionResult> DeleteStudent(Guid driverId)
    {
        var driver = await _unitOfWork.Students.GetAsync(driverId);

        if (driver == null)
        {
            return NotFound();
        }

        await _unitOfWork.Students.DeleteAsync(driverId);
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

        //await _notificationPublisherService.SendNotification(result.Id, result.RegistrationNumber);
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


