namespace Xero.Products.DataLayer.Tests;

public class ProductsDataFixture : IDisposable
{
    public ProductsContext context { get; private set; }
    public Guid[] ProductIds { get; private set; }
    public List<Product> Products { get; private set; }
    public List<ProductOption> ProductOptions { get; private set; }

    public ProductsDataFixture()
    {
        var options = new DbContextOptionsBuilder<ProductsContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        context = new ProductsContext(options);

        ProductIds = new Guid[3]
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        Products = new List<Product> {
            new Product { Id = ProductIds[0].ToString(), Name = "Product1", Description = "Description", DeliveryPrice = null, Price = 10 },
            new Product { Id = ProductIds[1].ToString(), Name = "Product2", Description = "Description", DeliveryPrice = null, Price = 30 },
            new Product { Id = ProductIds[2].ToString(), Name = "Product3", Description = "Description", DeliveryPrice = null, Price = 50 }
        };

        ProductOptions = new List<ProductOption> {
            new ProductOption { Id = Guid.NewGuid().ToString(), ProductId = ProductIds[0].ToString(), Name = "Option1_1", Description = "Description1_1" },
            new ProductOption { Id = Guid.NewGuid().ToString(), ProductId = ProductIds[0].ToString(), Name = "Option1_2", Description = "Description1_2" },
            new ProductOption { Id = Guid.NewGuid().ToString(), ProductId = ProductIds[1].ToString(), Name = "Option2_1", Description = "Description2_1" }
        };
        Init();
    }
    public void Init()
    {
        context.Products.AddRange(Products);
        context.ProductOptions.AddRange(ProductOptions);

        context.SaveChanges();
    }
    public void Reset()
    {
        context.Database.EnsureDeleted();
    }
    public void Dispose()
    {
        context.Dispose();
    }
}