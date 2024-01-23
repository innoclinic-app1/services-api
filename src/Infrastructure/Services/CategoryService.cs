using Domain.Dtos.Categories;
using Domain.Entities;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Interfaces.Services;
using Mapster;

namespace Infrastructure.Services;

public class CategoryService : BaseService<Category, CategoryDto, CategoryCreateDto, CategoryUpdateDto>,
    ICategoryService
{
    private readonly ICategoryRepository _repository;
    
    public CategoryService(ICategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }

    public override async Task<CategoryDto> UpdateAsync(int id, CategoryUpdateDto updateDto, 
        CancellationToken cancellation = default)
    {
        var category = updateDto.Adapt<Category>();
        category.Id = id;
        
        await _repository.UpdateAsync(category, cancellation);
        
        return category.Adapt<CategoryDto>();
    }
}
