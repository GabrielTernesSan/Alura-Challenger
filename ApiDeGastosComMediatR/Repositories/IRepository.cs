using System.Linq.Expressions;

namespace ApiDeGastosComMediatR.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> Get();

        Task Save();

        Task<T> GetById(Expression<Func<T, bool>> predicate);

        Task Add(T item);

        Task Update(T item);

        Task Delete(T item);
    }
}
