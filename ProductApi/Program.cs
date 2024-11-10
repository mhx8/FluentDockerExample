using Microsoft.Data.SqlClient;
using ProductApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<SqlConnection>(_ =>
{
    string? connectionString = builder.Configuration.GetConnectionString("Products");
    return new SqlConnection(connectionString);
});
builder.Services.AddTransient<ProductRepository>();

WebApplication app = builder.Build();

app.MapGet("/products", async (ProductRepository repository) =>
{
    IEnumerable<Product> products = await repository.GetAllProductsAsync();
    return Results.Ok(products);
});

app.MapGet("/products/{id:int}", async (int id, ProductRepository repository) =>
{
    Product? product = await repository.GetProductByIdAsync(id);
    return product is not null ? Results.Ok(product) : Results.NotFound();
});

app.Run();