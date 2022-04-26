namespace Xero.Products.DataLayer.Repositories;
public interface IProductRepository
{
    Task<Product?> GetProduct(string id);
    IAsyncEnumerable<Product> GetAllProducts(CancellationToken ct = default);
    Task InsertProduct(Product product, CancellationToken ct = default);
    Task UpdateProduct(Product product, CancellationToken ct = default);
    Task DeleteProduct(string id, CancellationToken ct = default);
    Task<bool> ProductExists(string id);
}
