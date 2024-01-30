using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public abstract class BaseService<TEntity, TDto, TCreateDto, TUpdateDto> : 
    IBaseService<TDto, TCreateDto, TUpdateDto> where TEntity : class
{
    private IBaseRepository<TEntity> Repository { get; }

    protected BaseService(IBaseRepository<TEntity> repository)
    {
        Repository = repository;
    }
    
    public async Task DeleteAsync(int id, CancellationToken cancellation = default)
    {
        await Repository.RemoveAsync(id, cancellation);
    }

    public async Task<TDto> GetOneAsync(int id, CancellationToken cancellation = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellation);

        return entity.Adapt<TDto>();
    }

    public async Task<ICollection<TDto>> GetManyAsync(int skip, int take, CancellationToken cancellation = default)
    {
        var entities = await Repository.GetManyAsync(skip, take, cancellation);

        return entities.Adapt<ICollection<TDto>>();
    }

    public async Task<TDto> CreateAsync(TCreateDto createDto, CancellationToken cancellation = default)
    {
        var entity = createDto.Adapt<TEntity>();

        await Repository.InsertAsync(entity, cancellation);
        
        return entity.Adapt<TDto>();
    }

    public abstract Task<TDto> UpdateAsync(int id, TUpdateDto updateDto, CancellationToken cancellation = default);
}
