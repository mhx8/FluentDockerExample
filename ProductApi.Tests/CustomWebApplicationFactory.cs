using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;

namespace ProductApi.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<IApiAssemblyMarker>
{
    private const string ConnectionString =
        "Server=localhost,1433;Database=Products;User Id=sa;Password=Password!123;Trust Server Certificate=true";

    protected override IHost CreateHost(
        IHostBuilder builder)
    {
        builder.UseEnvironment("Testing");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<SqlConnection>();
            services.AddTransient<SqlConnection>(_ => new SqlConnection(ConnectionString));
        });

        return base.CreateHost(builder);
    }
}