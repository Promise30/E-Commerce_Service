using ECommerceService.API.Data;
using ECommerceService.API.Database.Interface;
using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ECommerceService.API.Database.Implementation
{
    public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey> where T : Entity<Tkey>
    {
        private readonly ILogger<BaseRepository<T, Tkey>> _logger;
        private readonly DbSet<T> _dbSet;
        private readonly ECommerceDbContext _context;

        public BaseRepository(ECommerceDbContext context, ILogger<BaseRepository<T, Tkey>> logger)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _logger = logger;
        }

        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            _logger.LogInformation($"Entity with id {entity.Id} has been created at {DateTime.UtcNow}");
        }

        public async Task DeleteAsync(Tkey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _logger.LogInformation($"Entity with id {id} has been deleted at {DateTime.UtcNow}");
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return await _dbSet.Where(filter).AsNoTracking().ToListAsync();
            return await _dbSet.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> includeProperty = null)
        {
            var query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if(includeProperty != null)
            {
                query = query.Include(includeProperty);
            }
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties)
        {
            var query = _dbSet.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return await query.AsNoTracking().ToListAsync();
        }
        public async Task<PagedList<T>> GetAllPaginatedAsync(RequestParameters parameter, Expression<Func<T, bool>> filter = null)
        {
            var source = _dbSet.AsNoTracking();
            return await PagedList<T>.ToPagedList(source, filter, parameter);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter)
        {
            return await _dbSet.FirstOrDefaultAsync(filter);
        }
        public async Task<T> GetByIdAsync(Tkey id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task SaveChangesAsync(CancellationToken ct = default)
        {
             await _context.SaveChangesAsync(ct);
        }
        public Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }
        public async Task<PagedList<T>> GetAllPaginatedAsync(RequestParameters requestParameters, List<T> entities)
        {
            return await PagedList<T>.ToPagedListAsync(entities, requestParameters);
        }
    }
}
