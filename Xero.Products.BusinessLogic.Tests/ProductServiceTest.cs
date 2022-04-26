namespace Xero.Products.BusinessLogic.Tests;
public class ProductServiceTest
{
    protected CancellationToken _ct => It.IsAny<CancellationToken>();
    protected string _str => It.IsAny<string>();

    [Fact]
    public async Task GetAllProductsSuccess()
    {
        var productRepoMoq = new Mock<IProductRepository>();
        var productOptionRepoMoq = new Mock<IProductOptionsRepository>();
        var productList = new List<Entities.Product> { 
            new Entities.Product { Id = Guid.NewGuid().ToString(), Name ="Name1",Description=null,Price=100,DeliveryPrice=10},
            new Entities.Product { Id = Guid.NewGuid().ToString(), Name ="Name2",Description=null,Price=200,DeliveryPrice=20}
        }.ToAsyncEnumerable();
        
        productRepoMoq.Setup(x => x.GetAllProducts(_ct)).Returns(productList);

        var expectedProducts = new List<Models.Product>(); 
        await foreach(var product in productList)
        {
            expectedProducts.Add(product.ToModel());
        }

        var productService = new ProductService(productRepoMoq.Object, productOptionRepoMoq.Object);
        var products = await productService.GetProducts(_ct).ToListAsync();
        expectedProducts.Should().BeEquivalentTo(products);
    }
    [Fact]
    public async Task GetProductByIdSuccess()
    {
        var productRepoMoq = new Mock<IProductRepository>();
        var productOptionRepoMoq = new Mock<IProductOptionsRepository>();
        var product = new Entities.Product { Id = Guid.NewGuid().ToString(), Name = "Name1", Description = null, Price = 100, DeliveryPrice = 10 };

        productRepoMoq.Setup(x => x.GetProduct(_str)).ReturnsAsync(product);

        var expectedProduct = product.ToModel();

        var productService = new ProductService(productRepoMoq.Object, productOptionRepoMoq.Object);
        var productModel = await productService.GetProductById(Guid.Parse(product.Id));
        expectedProduct.Should().BeEquivalentTo(productModel);
    }
}
