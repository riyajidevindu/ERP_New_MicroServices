
using ERP.TranscriptGenetation.Core.Entity;

namespace ERP.TranscriptGenetation.Core.Interfaces
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
