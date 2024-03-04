using AutoMapper;
using ERP.ModuleRegistration.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ERP.ModuleRegistration.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IUnitOfWork _unitOfWork;
    protected IMapper _mapper;

    public BaseController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }



    

}
