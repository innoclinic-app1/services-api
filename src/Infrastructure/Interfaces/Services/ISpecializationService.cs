using Domain.Dtos.Specializations;

namespace Infrastructure.Interfaces.Services;

public interface ISpecializationService : IBaseService<SpecializationDto, SpecializationCreateDto, SpecializationUpdateDto>
{
    Task ChangeStatusAsync(int id, bool status, CancellationToken cancellation = default);
}
