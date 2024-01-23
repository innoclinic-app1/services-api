using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly DapperContext _context;

    public SpecializationRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task InsertAsync(Specialization specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "INSERT INTO specializations (name, is_active) VALUES (@Name, @IsActive)";
        
        var id = await connection.QuerySingleAsync<int>(query, specialization);
        specialization.Id = id;
    }

    public async Task RemoveAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "DELETE FROM specializations WHERE id = @Id";

        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task UpdateAsync(Specialization specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        const string query = "UPDATE specializations SET name = @Name, is_active = @IsActive WHERE id = @Id";
            
        await connection.ExecuteAsync(query, specialization);
    }

    public async Task<Specialization> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "SELECT * FROM specializations WHERE id = @Id";
        
        var specialization = await connection.QuerySingleOrDefaultAsync<Specialization>(query, new { Id = id });

        if (specialization != null)
        {
            return specialization;
        }
        
        throw new NotFoundException<Specialization>(id);
    }

    public async Task<IEnumerable<Specialization>> GetManyAsync(int skip, int take,
        CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "SELECT * FROM specializations ORDER BY id DESC " +
                             "OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";
        
        return await connection.QueryAsync<Specialization>(query, new { Skip = skip, Take = take });
    }

    public async Task ChangeStatusAsync(int id, bool status, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "UPDATE specializations SET is_active = @Status WHERE id = @Id";
        
        await connection.ExecuteAsync(query, new { Id = id, Status = status });
    }
}
