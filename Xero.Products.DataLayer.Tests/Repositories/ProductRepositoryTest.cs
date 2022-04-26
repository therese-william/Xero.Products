namespace Xero.Products.DataLayer.Tests.Repositories;

public class ProductRepositoryTest : RepositoryTestBase
{
    protected CancellationToken _ct => It.IsAny<CancellationToken>();

    public ProductRepositoryTest(ProductsDataFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task GetAllProducts_Success()
    {
        var productRepository = new ProductRepository(ProductsContext);
        var expectedCount = ProductIds.Count();
        var products = await productRepository.GetAllProducts(_ct).ToListAsync();
        var productsCount = products.Count();
        Assert.Equal(expectedCount,productsCount);
    }

    [Fact]
    public async Task GetProduct_Success()
    {
        var productRepository = new ProductRepository(ProductsContext);
        var expectedProduct = ProductList.First();
        var product = await productRepository.GetProduct(expectedProduct.Id);
        product.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task CreateProduct_Success()
    {
        var productRepository = new ProductRepository(ProductsContext);
        var expectedProduct = new Product
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Name",
            Description = "Description",
            Price = 100,
            DeliveryPrice = 10
        };
        await productRepository.InsertProduct(expectedProduct,_ct);
        var insertedProduct = await ProductsContext.Products.FindAsync(expectedProduct.Id);
        insertedProduct.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task UpdateProduct_Success()
    {
        var productRepository = new ProductRepository(ProductsContext);
        var expectedProduct = ProductList.First();
        expectedProduct.Name = "Updated";
        await productRepository.UpdateProduct(expectedProduct,_ct);
        var updatedProduct = await ProductsContext.Products.FindAsync(expectedProduct.Id);
        updatedProduct.Should().BeEquivalentTo(expectedProduct);
    }

    [Fact]
    public async Task DeleteProduct_Success()
    {
        var productRepository = new ProductRepository(ProductsContext);
        var deletedProduct = ProductList.First();
        await productRepository.DeleteProduct(deletedProduct.Id,_ct);
        var product = await ProductsContext.Products.FindAsync(deletedProduct.Id);
        product.Should().BeNull();
    }
}
