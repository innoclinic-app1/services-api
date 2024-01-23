using Domain.Dtos.Categories;

namespace Infrastructure.Interfaces.Services;

public interface ICategoryService : IBaseService<CategoryDto, CategoryCreateDto, CategoryUpdateDto>;
