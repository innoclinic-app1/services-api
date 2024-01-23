using Domain.Dtos.Services;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class ServiceService : BaseService<Service, ServiceDto, ServiceCreateDto, ServiceUpdateDto>, IServiceService
{
    private IServiceRepository Repository { get; }
    
    public ServiceService(IServiceRepository repository) : base(repository)
    {
        Repository = repository;
    }

    public override async Task<ServiceDto> UpdateAsync(int id, ServiceUpdateDto updateDto, CancellationToken cancellation = default)
    {
        var service = updateDto.Adapt<Service>();
        service.Id = id;
        
        await Repository.UpdateAsync(service, cancellation);
        
        return service.Adapt<ServiceDto>();
    }
}
