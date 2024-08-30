using ERP.StudentRegistration.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.StudentRegistration.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> UpdateAsync(T entity);
    }
}
