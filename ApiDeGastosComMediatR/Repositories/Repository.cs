using ApiDeGastosComMediatR.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiDeGastosComMediatR.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(T item)
        {
            await Task.Run(() => _context.Set<T>().Add(item));
        }

        public async Task Delete(T item)
        {
            await Task.Run(() => _context.Set<T>().Remove(item));
        }
        public async Task Save()
        {
            await Task.Run(() => _context.SaveChangesAsync());
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await Task.Run(() => _context.Set<T>().ToList());
        }

        public async Task<T> GetById(Expression<Func<T, bool>> predicate)
        {
            return await Task.Run(() => _context.Set<T>().AsNoTracking().SingleOrDefaultAsync(predicate));
        }

        public async Task Update(T item)
        {
            await Task.Run(() =>
            {
                _context.Entry(item).State = EntityState.Modified;
                _context.Set<T>().Update(item);
            });
        }
    }
}
