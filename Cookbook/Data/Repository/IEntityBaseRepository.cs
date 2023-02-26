using System.Collections;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Cookbook.Data.Repository
{
    public interface IEntityBaseRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int returnedRecords);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAndSaveAsync(T entity);
        Task UpdateAndSaveAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
