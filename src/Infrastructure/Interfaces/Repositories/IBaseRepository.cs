namespace Infrastructure.Interfaces.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);

    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
    
    Task RemoveAsync(int id, CancellationToken cancellationToken = default);
    
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<T>> GetManyAsync(int skip, int take, CancellationToken cancellationToken = default);
}
