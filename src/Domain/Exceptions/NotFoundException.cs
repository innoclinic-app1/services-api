namespace Domain.Exceptions;

public class NotFoundException<T> : InvalidOperationException where T : class
{
    public NotFoundException(int id) : base($"Entity of type {typeof(T).Name} with id {id} was not found.")
    {
    }
}
