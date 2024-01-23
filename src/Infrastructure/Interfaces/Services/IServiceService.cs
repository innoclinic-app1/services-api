using Domain.Dtos.Services;

namespace Infrastructure.Interfaces.Services;

public interface IServiceService : IBaseService<ServiceDto, ServiceCreateDto, ServiceUpdateDto>;
