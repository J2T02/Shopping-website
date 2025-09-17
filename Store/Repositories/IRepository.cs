namespace Store.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
        Task SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
