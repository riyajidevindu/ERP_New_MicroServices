using AutoMapper;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class LabControllers : BaseController
    {
        public LabControllers(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
