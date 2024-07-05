using AutoMapper;
using ERP.TrainingManagement.Core.DTOs.Requests;
using ERP.TrainingManagement.Core.DTOs.Responses;
using ERP.TrainingManagement.Core.Entities;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ERP.TrainingManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalRequestController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ApprovalRequestController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> CreateApprovalRequest([FromBody] CreateApprovalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var approvalRequest = _mapper.Map<ApprovalRequest>(request);
            approvalRequest.CreatedDate = DateTime.UtcNow;
            approvalRequest.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.AddApprovalRequestRepository.Add(approvalRequest);
            await _unitOfWork.CompleteAsync();

            var result = _mapper.Map<GetApprovalRequestResponse>(approvalRequest);
            return CreatedAtAction(nameof(GetApprovalRequestById), new { id = approvalRequest.Id }, result);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Student,Coordinator")]
        public async Task<IActionResult> GetApprovalRequestById(Guid id)
        {
            var approvalRequest = await _unitOfWork.AddApprovalRequestRepository.GetById(id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<GetApprovalRequestResponse>(approvalRequest);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateApprovalRequest(Guid id, [FromBody] UpdateApprovalRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingApprovalRequest = await _unitOfWork.AddApprovalRequestRepository.GetById(id);
            if (existingApprovalRequest == null)
            {
                return NotFound();
            }

            _mapper.Map(request, existingApprovalRequest);
            existingApprovalRequest.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.AddApprovalRequestRepository.Update(existingApprovalRequest);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> DeleteApprovalRequest(Guid id)
        {
            var approvalRequest = await _unitOfWork.AddApprovalRequestRepository.GetById(id);
            if (approvalRequest == null)
            {
                return NotFound();
            }

            await _unitOfWork.AddApprovalRequestRepository.Delete(id);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet("All")]
        [Authorize(Roles = "Coordinator")]
        public async Task<IActionResult> GetAllApprovalRequests()
        {
            var approvalRequests = await _unitOfWork.AddApprovalRequestRepository.All();
            var result = _mapper.Map<IEnumerable<GetApprovalRequestResponse>>(approvalRequests);
            return Ok(result);
        }

        //[HttpGet("ByStatus/{status}")]
        //[Authorize(Roles = "Coordinator")]
        //public async Task<IActionResult> GetApprovalRequestsByStatus(int status)
        //{
        //    var approvalRequests = await _unitOfWork.AddApprovalRequestRepository
        //    var result = _mapper.Map<IEnumerable<GetApprovalRequestResponse>>(approvalRequests);
        //    return Ok(result);
        //}
    }
}
