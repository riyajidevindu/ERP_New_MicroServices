using AutoMapper;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class ScheduledLabControllers : BaseController
    {
        public ScheduledLabControllers(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
