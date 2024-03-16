using ERP.Authentication.Core.Entity;

namespace ERP.Authentication.Core.Interfaces
{
    public interface IUserAccountRepository : IGenericRepository<UserAccount>
    {
        public UserAccount GetUserAccount(string username, string password);
    }
}
