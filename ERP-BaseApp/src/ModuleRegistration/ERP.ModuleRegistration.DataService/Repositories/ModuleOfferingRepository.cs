using ERP.ModuleRegistration.Core.Entity;
using ERP.ModuleRegistration.DataService.Repositories;
using ERP.ModuleRegistration.DataService;
using Microsoft.Extensions.Logging;
using ERP.ModuleRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class ModuleOfferingRepository : GenericRepository<ModuleOffering>, IModuleOfferingRepository
    {
        public ModuleOfferingRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
