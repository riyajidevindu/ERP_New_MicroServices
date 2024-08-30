using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ERP.LabEquipmentManagement.DataService.Repositories.Interfaces;

namespace ERP.LabEquipmentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

    }
}

