using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.TrainingManagement.DataServices.Data;
using ERP.TrainingManagement.DataServices.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace ERP.TrainingManagement.DataServices.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly ILogger _logger;
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context,ILogger logger)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logger = logger;
        }

        public async Task<IEnumerable<T>> All()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await GetById(id);
            if (entity == null)
            {
                return false;
            }

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
