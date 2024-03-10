using ERP.TranscriptGenetation.Core.Entity;
using Microsoft.Extensions.Logging;
using ERP.TranscriptGenetation.Core.Interfaces;

namespace ERP.TranscriptGeneration.DataService.Repositories
{
    public class ResultRepository : GenericRepository<Result>, IResultRepository
    {
        public ResultRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {
        }


    }
}
