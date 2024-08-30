using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Authentication.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserAccountRepository UserAccounts { get; }
        
        Task<bool> CompleteAsync();
    }
}
