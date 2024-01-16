using Domain.Entities;

namespace Infrastructure.Interfaces.Repositories;

public interface ISpecializationRepository : IBaseRepository<Specialization>
{
    Task ChangeStatusAsync(int id, bool status);
}
