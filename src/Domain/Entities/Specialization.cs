namespace Domain.Entities;

public class Specialization
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
}
