namespace Xero.Products.DataLayer.Tests.Repositories;

public class RepositoryTestBase : IClassFixture<ProductsDataFixture>
{
    protected readonly ProductsContext ProductsContext;
    protected readonly Guid[] ProductIds;
    protected readonly List<Product> ProductList;
    protected readonly List<ProductOption> ProductOptions;
    private readonly ProductsDataFixture _fixture;

    public RepositoryTestBase(ProductsDataFixture fixture)
    {
        _fixture = fixture;
        ProductsContext = fixture.context;
        ProductIds = fixture.ProductIds;
        ProductList = fixture.Products;
        ProductOptions = fixture.ProductOptions;
    }

    public void LoadData()
    {
        _fixture.Init();
    }
    public void ResetData()
    {
        _fixture.Reset();
    }
}
