using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SanmolTaskManager_DAL.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // --------------------- READ ---------------------
        Task<T> GetByIdAsync(int id);
        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize);
        IQueryable<T> Query();

        // --------------------- WRITE ---------------------
        Task AddAsync(T entity);
        void Update(T entity);

        // --------------------- SAVE ---------------------
        Task SaveAsync();
    }
}
