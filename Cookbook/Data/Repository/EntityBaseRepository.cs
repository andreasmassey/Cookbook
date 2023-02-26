using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Cookbook.Data.Repository
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, new()
    {
        private readonly DbContext _context;

        public EntityBaseRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync() 
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e);
                throw;
            }      
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate, int returnedRecords)
        {
            try
            {
                return await _context.Set<T>().Where(predicate).Take(returnedRecords).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                var result = await _context.Set<T>().FirstOrDefaultAsync(predicate);
                if (result != null)
                {
                    _context.Entry(result).State = EntityState.Detached;
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task AddAndSaveAsync(T entity)
        {
            try
            {
                _context.Entry<T>(entity);
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task UpdateAndSaveAsync(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Modified;
                await _context.SaveChangesAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual async Task DeleteAsync(T entity)
        {
            try
            {
                EntityEntry dbEntityEntry = _context.Entry<T>(entity);
                dbEntityEntry.State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                _context.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
