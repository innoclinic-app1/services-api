using Domain.Dtos.Services;
using Domain.Entities;
using Infrastructure.Contracts;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;
using MassTransit;

namespace Infrastructure.Services;

public class ServiceService : BaseService<Service, ServiceDto, ServiceCreateDto, ServiceUpdateDto>, IServiceService
{
    private IServiceRepository Repository { get; }
    private IPublishEndpoint PublishEndpoint { get; }
    
    public ServiceService(IServiceRepository repository, IPublishEndpoint publishEndpoint) : base(repository)
    {
        Repository = repository;
        PublishEndpoint = publishEndpoint;
    }

    public new async Task DeleteAsync(int id, CancellationToken cancellation = default)
    {
        await Repository.RemoveAsync(id, cancellation);
        
        await PublishEndpoint.Publish<ServiceDeletedEvent>(new { Id = id }, cancellation);
    }

    public override async Task<ServiceDto> UpdateAsync(int id, ServiceUpdateDto updateDto, CancellationToken cancellation = default)
    {
        var service = updateDto.Adapt<Service>();
        service.Id = id;
        
        await Repository.UpdateAsync(service, cancellation);
        
        return service.Adapt<ServiceDto>();
    }
}
