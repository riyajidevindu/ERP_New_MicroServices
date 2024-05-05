using AutoMapper;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class LabCoordinatorControllers : BaseController
    {
        public LabCoordinatorControllers(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
