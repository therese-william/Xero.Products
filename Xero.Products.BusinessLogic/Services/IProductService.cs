
namespace Xero.Products.BusinessLogic.Services
{
    public interface IProductService
    {
        IAsyncEnumerable<Product> GetProducts(CancellationToken ct = default);
        Task<Product?> GetProductById(Guid productId);
        Task DeleteProduct(Guid productId, CancellationToken ct = default);
        Task SaveProduct(Product product, Guid? productId = null, CancellationToken ct = default);
        IAsyncEnumerable<ProductOption> GetProductOptions(Guid productId, CancellationToken ct = default);
        Task<ProductOption?> GetProductOptionById(Guid optionId);
        Task SaveProductOption(ProductOption productOption, Guid? optionId = null, CancellationToken ct = default);
        Task DeleteProductOption(Guid optionId, CancellationToken ct = default);

    }
}