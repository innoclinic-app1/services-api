namespace Infrastructure.Interfaces.Repositories;

public interface IBaseRepository<T> where T: class
{
    Task InsertAsync(T specialization, CancellationToken cancellation = default);
    
    Task RemoveAsync(int id, CancellationToken cancellation = default);
    
    Task UpdateAsync(T specialization, CancellationToken cancellation = default);
    
    Task<T> GetByIdAsync(int id, CancellationToken cancellation = default);
    
    Task<IEnumerable<T>> GetManyAsync(int skip, int take, CancellationToken cancellation = default);
}
