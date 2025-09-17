using Microsoft.EntityFrameworkCore;
using Store.Database;
using Store.Entity;
using System.Threading.Tasks;

namespace Store.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, BaseEntity
    {
        private readonly ShopContext _shopContext;
        private readonly DbSet<T> _dbSet;
        public Repository(ShopContext shopContext)
        {
            _shopContext = shopContext;
            _dbSet = _shopContext.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

        }

        public async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public IQueryable<T> Get()
        {
            return _dbSet.Where(x => true);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            await _shopContext.SaveChangesAsync(cancellationToken);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
