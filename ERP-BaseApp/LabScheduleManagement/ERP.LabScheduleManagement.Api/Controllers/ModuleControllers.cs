using AutoMapper;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class ModuleControllers : BaseController
    {
        public ModuleControllers(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
