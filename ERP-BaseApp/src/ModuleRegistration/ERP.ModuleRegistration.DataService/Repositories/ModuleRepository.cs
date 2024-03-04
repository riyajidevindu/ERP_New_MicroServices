using ERP.ModuleRegistration.Core.Entity;
using ERP.ModuleRegistration.DataService.Repositories;
using ERP.ModuleRegistration.Core.Interfaces;
using ERP.StudentRegistration.DataService.Data;
using Microsoft.Extensions.Logging;

namespace ERP.StudentRegistration.DataService.Repositories
{
    public class ModuleRepository : GenericRepository<Module>, IModuleRepository
    {
        public ModuleRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
