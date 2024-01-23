using Dapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces.Repositories;

namespace Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly DapperContext _context;

    public CategoryRepository(DapperContext context)
    {
        _context = context;
    }
    
    public async Task InsertAsync(Category specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "INSERT INTO categories (name) VALUES (@Name)";
        
        var id = await connection.QuerySingleAsync<int>(query, specialization);
        specialization.Id = id;
    }

    public async Task RemoveAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "DELETE FROM categories WHERE id = @Id";

        await connection.ExecuteAsync(query, new { Id = id });
    }

    public async Task UpdateAsync(Category specialization, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "UPDATE categories SET name = @Name WHERE id = @Id";
        
        await connection.ExecuteAsync(query, specialization);
    }

    public async Task<Category> GetByIdAsync(int id, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "SELECT * FROM categories WHERE id = @Id";
        
        var category = await connection.QuerySingleOrDefaultAsync<Category>(query, new { Id = id });

        if (category != null)
        {
            return category;
        }
        
        throw new NotFoundException<Category>(id);
    }

    public async Task<IEnumerable<Category>> GetManyAsync(int skip, int take, CancellationToken cancellation = default)
    {
        using var connection = _context.CreateConnection();
        
        const string query = "SELECT * FROM categories ORDER BY id OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

        return await connection.QueryAsync<Category>(query, new { Skip = skip, Take = take });
    }
}
