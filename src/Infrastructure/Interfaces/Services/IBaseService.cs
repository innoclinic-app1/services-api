namespace Infrastructure.Interfaces.Services;

public interface IBaseService<TDto, in TCreateDto, in TUpdateDto>
{
    Task DeleteAsync(int id, CancellationToken cancellation = default);
    Task<TDto> GetOneAsync(int id, CancellationToken cancellation = default);
    Task<ICollection<TDto>> GetManyAsync(int skip, int take, CancellationToken cancellation = default);
    Task<TDto> CreateAsync(TCreateDto createDto, CancellationToken cancellation = default);
    Task<TDto> UpdateAsync(int id, TUpdateDto updateDto, CancellationToken cancellation = default);
}
