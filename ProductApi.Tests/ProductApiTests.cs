using System.Net.Http.Json;

namespace ProductApi.Tests;

public class ProductApiTests : CustomWebApplicationFactory, IClassFixture<DockerComposeFixture>
{
    public ProductApiTests(DockerComposeFixture dockerComposeFixture)
    {
        dockerComposeFixture.InitDockerHost(FileUtils.GetDockerComposePath());
    }
    
    [Fact]
    public async Task GetProducts_ShouldReturnProducts()
    {
        // Arrange
        HttpClient client = CreateClient();
        
        // Act
        HttpResponseMessage response = await client.GetAsync("/products");
        
        // Assert
        response.EnsureSuccessStatusCode();
        IEnumerable<Product>? products = await response.Content.ReadFromJsonAsync<IEnumerable<Product>>();
        Assert.NotNull(products);
        Assert.NotEmpty(products);
    }
    
    [Fact]
    public async Task GetProduct_ShouldReturnProduct()
    {
        // Arrange
        HttpClient client = CreateClient();
        
        // Act
        HttpResponseMessage response = await client.GetAsync("/products/1");
        
        // Assert
        response.EnsureSuccessStatusCode();
        Product? product = await response.Content.ReadFromJsonAsync<Product>();
        Assert.NotNull(product);
        Assert.Equal(1, product.Id);
    }
}