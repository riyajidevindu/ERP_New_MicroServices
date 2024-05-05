using AutoMapper;
using ERP.LabScheduleManagement.DataServices.Repositories.Interfaces;

namespace ERP.LabScheduleManagement.Api.Controllers
{
    public class LabEquipmentControllers : BaseController
    {
        public LabEquipmentControllers(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
