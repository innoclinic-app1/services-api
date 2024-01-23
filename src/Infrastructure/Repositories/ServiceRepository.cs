using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class ServiceRepository : IServiceRepository
{
    private readonly DapperContext _context;
    
    public ServiceRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task InsertAsync(Service specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "INSERT INTO services (category_id, specialization_id, name, price, is_active) " + 
                             "VALUES (@CategoryId, @SpecializationId, @Name, @Price, @IsActive)";

        var id = await connection.QuerySingleAsync<int>(query, specialization);
        
        specialization.Id = id;
    }

    public async Task RemoveAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "DELETE FROM services WHERE id = @Id";
        
        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task UpdateAsync(Service specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "UPDATE services SET category_id = @CategoryId, specialization_id = @SpecializationId, " +
                             "name = @Name, price = @Price, is_active = @IsActive WHERE id = @Id";

        await connection.ExecuteAsync(query, specialization);
    }

    public async Task<Service> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "SELECT * FROM services WHERE id = @Id";

        var service = await connection.QuerySingleOrDefaultAsync<Service>(query, new { Id = id });

        if (service != null)
        {
            return service;
        }

        throw new NotFoundException<Service>(id);
    }

    public async Task<IEnumerable<Service>> GetManyAsync(int skip, int take, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();

        const string query = "SELECT * FROM services S " +
                             "LEFT JOIN categories C ON S.CategoryId=C.Id " +
                             "LEFT JOIN specializations SP ON S.SpecializationId = SP.Id " +
                             "ORDER BY id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";
        
        return await connection.QueryAsync<Service>(query, new { Skip = skip, Take = take });
    }
}
