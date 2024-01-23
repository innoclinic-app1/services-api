namespace Domain.Dtos.Services;

public class ServiceCreateDto
{
    public int CategoryId { get; set; }
    public int SpecializationId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}
