using Dapper;
using Microsoft.Data.SqlClient;
using ProductApi;

public class ProductRepository(
    SqlConnection connection)
{
    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        const string query = "SELECT * FROM [dbo].[Product]";
        return await connection.QueryAsync<Product>(query);
    }

    public async Task<Product?> GetProductByIdAsync(
        int id)
    {
        const string query = "SELECT * FROM [dbo].[Product] WHERE Id = @Id";
        return await connection.QuerySingleOrDefaultAsync<Product>(query, new { Id = id });
    }
}