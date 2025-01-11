using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using System.Linq.Expressions;

namespace ECommerceService.API.Database.Interface
{
    public interface IBaseRepository<T, Tkey> where T: Entity<Tkey>
    {
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Tkey id);

        Task<T> GetByIdAsync(Tkey id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);

        Task<T> GetLastItemAsync();
        Task<Tkey> GetLastItemId();

        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> includeProperty = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
        Task<PagedList<T>> GetAllPaginatedAsync(RequestParameters parameter, Expression<Func<T, bool>> filter = null);
        Task<PagedList<T>> GetAllPaginatedAsync(RequestParameters parameter, SortParameters sortParams, Expression<Func<T, bool>> filter = null);
        Task<PagedList<T>> GetAllPaginatedAsync(RequestParameters parameter, List<T> entity, Expression<Func<T, bool>> filter = null);

        Task SaveChangesAsync(CancellationToken ct = default);
    }
}
