using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetById;
using ERP.LabScheduleManagement.Core.DTOs.Requests.CreateRequests;
using ERP.LabScheduleManagement.Core.Entities;
using ERP.LabScheduleManagement.Core.DTOs.Requests.UpdateRequests;
using ERP.LabScheduleManagement.Core.DTOs.Responses.GetAll;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class ModuleControllers : BaseController
    {
        public ModuleControllers(
            IUnitOfWork unitOfWork, 
            IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        [HttpGet]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> GetModule(Guid moduleId)
        {
            var module = await _unitOfWork.Modules.GetById(moduleId);

            if (module == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetModuleByIdResponse>(module);

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddModule([FromBody] CreateModuleRequest module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = _mapper.Map<Module>(module);
            await _unitOfWork.Modules.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetModule), new { moduleId = result.Id }, result);

        }


        [HttpPut]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> UpdateModule(Guid moduleId, [FromBody] UpdateModuleRequest module)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var existingModule = _mapper.Map<Module>(module);
                await _unitOfWork.Modules.Update(existingModule);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the module.");
            }

            return NoContent();

        }

        [HttpGet("Get")]
        public async Task<IActionResult> GetAllModule()
        {
            var modules = await _unitOfWork.Modules.All();

            if (modules == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<IEnumerable<GetModuleResponse>>(modules);

            return Ok(result);
        }

        [HttpDelete]
        [Route("{moduleId:guid}")]
        public async Task<IActionResult> DeleteModule(Guid moduleId)
        {
            var module = await _unitOfWork.Modules.GetById(moduleId);

            if (module == null)
            {
                return NotFound();
            }

            await _unitOfWork.Modules.Delete(moduleId);
            await _unitOfWork.CompleteAsync();

            return NoContent();


        }
    }
}
