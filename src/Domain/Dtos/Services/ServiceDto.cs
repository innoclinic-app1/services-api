using Domain.Dtos.Categories;
using Domain.Dtos.Specializations;

namespace Domain.Dtos.Services;

public class ServiceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    
    public CategoryDto Category { get; set; } = null!;
    public SpecializationDto Specialization { get; set; } = null!;
}
