using ERP.Authentication.Core.Entity;
using ERP.Authentication.Core.Interfaces;
using ERP.Authentication.DataService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERP.Authentication.DataService.Repositories
{
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(AppDbContext context, ILogger logger) : base(context, logger)
        {

        }

        public UserAccount GetUserAccount(string username, string password)
        {
            return _context.UserAccounts.Where(u => u.UserName == username && u.Password == password).FirstOrDefault() ?? null;
        }

        public override async Task<bool> UpdateAsync(UserAccount entity)
        {
            try
            {
                var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (result == null)
                    return false;

                result.UserName = entity.UserName;
                result.Password = entity.Password;
                result.Role = entity.Role;
                result.UpdateDate = DateTime.UtcNow;

                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Update function error",
                    typeof(UserAccountRepository));
                throw;
            }
        }
    }
}
