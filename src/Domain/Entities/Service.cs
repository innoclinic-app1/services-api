namespace Domain.Entities;

public class Service
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
    public int SpecializationId { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}
