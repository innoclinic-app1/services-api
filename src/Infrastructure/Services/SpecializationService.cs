using Domain.Dtos.Specializations;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class SpecializationService : BaseService<Specialization, 
    SpecializationDto, SpecializationCreateDto, SpecializationUpdateDto>, ISpecializationService
{
    private ISpecializationRepository Repository { get; }
    
    public SpecializationService(ISpecializationRepository repository) : base(repository)
    {
        Repository = repository;
    }

    public override async Task<SpecializationDto> UpdateAsync(int id, SpecializationUpdateDto updateDto,
        CancellationToken cancellation = default)
    {
        var specialization = updateDto.Adapt<Specialization>();
        specialization.Id = id;

        await Repository.UpdateAsync(specialization, cancellation);

        return specialization.Adapt<SpecializationDto>();
    }

    public async Task ChangeStatusAsync(int id, bool status, CancellationToken cancellation = default)
    {
        await Repository.ChangeStatusAsync(id, status, cancellation);
    }
}
