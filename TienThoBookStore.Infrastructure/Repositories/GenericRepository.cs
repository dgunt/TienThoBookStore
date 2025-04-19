using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Infrastructure.Contexts;

namespace TienThoBookStore.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly TienThoBookStoreDbContext _context;

        public GenericRepository(TienThoBookStoreDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            //throw new NotImplementedException();
            await _context.Set<T>().AddAsync(entity);
        }

        public Task Delete(T entity)
        {
            //throw new NotImplementedException();
            _context.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            // throw new NotImplementedException();
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //throw new NotImplementedException();
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            //throw new NotImplementedException();
            return await _context.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Query()  // <-- hiện thực method mới
        => _context.Set<T>().AsQueryable();

        public Task Update(T entity)
        {
            //throw new NotImplementedException();
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
    }
}
