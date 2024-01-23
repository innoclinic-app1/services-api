using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure;

public class DapperContext
{
    private string ConnectionString { get; }
    
    public DapperContext(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public IDbConnection CreateConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}
