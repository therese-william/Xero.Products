namespace Xero.Products.App.Tests.Controllers;

public class ProductControllerTest : TestBase
{
    Product _anyProduct => It.IsAny<Product>();
    IAsyncEnumerable<Product> _anyProductList => It.IsAny<IAsyncEnumerable<Product>>();

    public ProductControllerTest(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Get_Success()
    {
        var expectedProductList = new List<Product>()
        {
            new Product{ Id = Guid.NewGuid(), Name = "Product1", Description = "Description1", Price = 99.99m, DeliveryPrice=9.99m },
            new Product{ Id = Guid.NewGuid(), Name = "Product2", Description = "Description2", Price = 199.99m, DeliveryPrice=19.99m }
        };
        
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts(_ct))
            .Returns(expectedProductList.ToAsyncEnumerable());

        var productController = new ProductsController(mockProductService.Object);

        var productsReturnObject = await productController.Get();
        var products = productsReturnObject.As<OkObjectResult>().Value.As<List<Product>>();

        Assert.IsType<OkObjectResult>(productsReturnObject);
        expectedProductList.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task GetById_Success()
    {
        var expectedProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product1",
            Description = "Description1",
            Price = 99.99m,
            DeliveryPrice = 9.99m
        };

        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProductById(_anyGuid))
            .ReturnsAsync(expectedProduct);

        var productController = new ProductsController(mockProductService.Object);

        var productReturnObject = await productController.Get(expectedProduct.Id);
        var product = productReturnObject.As<OkObjectResult>().Value.As<Product>();

        Assert.IsType<OkObjectResult>(productReturnObject);
        expectedProduct.Should().BeEquivalentTo(product);
    }

    [Fact]
    public async Task Get_EndToEnd_Success()
    {
        var expectedProductList = new List<Product>()
        {
            new Product{ Id = Guid.NewGuid(), Name = "Product1", Description = "Description1", Price = 99.99m, DeliveryPrice=9.99m },
            new Product{ Id = Guid.NewGuid(), Name = "Product2", Description = "Description2", Price = 199.99m, DeliveryPrice=19.99m }
        };

        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts(_ct))
            .Returns(expectedProductList.ToAsyncEnumerable());

        AppFactory.ConfigureTestServices(services => { 
            services.ReplaceSingleton<IProductService>(mockProductService.Object);
        });
        using var client = CreateUserAuthorizedClient();
        var response = await client.GetAsync($"{Urls.ProductUrl}");
        var products = await response.Deserialize<List<Product>>();
        Assert.True(response.StatusCode == HttpStatusCode.OK);
        expectedProductList.Should().BeEquivalentTo(products);
    }

    [Fact]
    public async Task GetById_EndToEnd_Success()
    {
        var expectedProduct = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Product1",
            Description = "Description1",
            Price = 99.99m,
            DeliveryPrice = 9.99m
        };

        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProductById(_anyGuid))
            .ReturnsAsync(expectedProduct);

        AppFactory.ConfigureTestServices(services => {
            services.ReplaceSingleton<IProductService>(mockProductService.Object);
        });
        using var client = CreateUserAuthorizedClient();
        var response = await client.GetAsync($"{Urls.ProductUrl}/{expectedProduct.Id.ToString()}");

        var product = await response.Deserialize<Product>();
        Assert.True(response.StatusCode == HttpStatusCode.OK);
        expectedProduct.Should().BeEquivalentTo(product);
    }

    [Theory]
    [MemberData(nameof(Create_UnauthorizedRequests_Data))]
    public async Task EndToEnd_Unauthorized_Requests(string method, string url, HttpContent content)
    {
        var mockProductService = new Mock<IProductService>();

        AppFactory.ConfigureTestServices(services => {
            services.ReplaceSingleton<IProductService>(mockProductService.Object);
        });
        using var client = CreateUnauthorizedClient();
        var response = method switch { 
            "GET" => await client.GetAsync(url),
            "POST" => await client.PostAsync(url, content),
            "PUT" => await client.PutAsync(url, content),
            "DELETE" => await client.DeleteAsync(url),
            _ => throw new Exception("Method not implemented!")
        };
        Assert.True(response.StatusCode == HttpStatusCode.Unauthorized);
    }

    public static IEnumerable<object[]> Create_UnauthorizedRequests_Data()
    {
        var _anyProduct = It.IsAny<Product>();
        var _any = It.IsAny<Object>();

        var requests = new[]
        {
            new
            {
                Method = HttpMethod.Get.Method,
                Url = Urls.ProductUrl,
                httpContent=HttpClientExtensions.CreateJsonContent(_any)
            },
            new
            {
                Method = HttpMethod.Get.Method,
                Url = $"{Urls.ProductUrl}/{Guid.NewGuid()}",
                httpContent=HttpClientExtensions.CreateJsonContent(_any)
            },
            new
            {
                Method = HttpMethod.Post.Method,
                Url = Urls.ProductUrl,
                httpContent= HttpClientExtensions.CreateJsonContent(_anyProduct)
            },
            new
            {
                Method = HttpMethod.Put.Method,
                Url = $"{Urls.ProductUrl}/{Guid.NewGuid()}",
                httpContent=HttpClientExtensions.CreateJsonContent(_anyProduct)
            },
            new
            {
                Method = HttpMethod.Delete.Method,
                Url = $"{Urls.ProductUrl}/{Guid.NewGuid()}",
                httpContent=HttpClientExtensions.CreateJsonContent(_any)
            }
        };
        for (int i = 0; i < requests.Length; i++)
        {
            yield return new object[] { requests[i].Method, requests[i].Url, requests[i].httpContent };
        }
    }

    [Fact]
    public async Task Get_EndToEnd_Forbidden()
    {
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProducts(_ct))
            .Returns(_anyProductList);

        AppFactory.ConfigureTestServices(services => {
            services.ReplaceSingleton<IProductService>(mockProductService.Object);
        });
        using var client = CreateGuestAuthorizedClient();
        var response = await client.GetAsync($"{Urls.ProductUrl}");
        Assert.True(response.StatusCode == HttpStatusCode.Forbidden);
    }

    [Fact]
    public async Task GetById_EndToEnd_BadRequest()
    {
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(x => x.GetProductById(_anyGuid))
            .ReturnsAsync(_anyProduct);

        AppFactory.ConfigureTestServices(services => {
            services.ReplaceSingleton<IProductService>(mockProductService.Object);
        });
        using var client = CreateUserAuthorizedClient();
        var response = await client.GetAsync($"{Urls.ProductUrl}/NonGuid");
        Assert.True(response.StatusCode == HttpStatusCode.BadRequest);
    }
}
