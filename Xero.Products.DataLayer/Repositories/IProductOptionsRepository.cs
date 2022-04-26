namespace Xero.Products.DataLayer.Repositories;

public interface IProductOptionsRepository
{
    IAsyncEnumerable<ProductOption> GetProductOptions(string productId,CancellationToken ct = default);
    Task<ProductOption?> GetProductOption(string optionId,CancellationToken ct = default);
    Task InsertProductOption(ProductOption productOption, CancellationToken ct = default);
    Task UpdateProductOption(ProductOption productOption, CancellationToken ct = default);
    Task DeleteProductOption(string optionId, CancellationToken ct = default);
    Task<bool> ProductOptionExists(string optionId, CancellationToken ct = default);
}
