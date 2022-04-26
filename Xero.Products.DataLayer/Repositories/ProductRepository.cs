namespace Xero.Products.DataLayer.Repositories;

public class ProductRepository : IProductRepository
{
    readonly ProductsContext _context;

    public ProductRepository(ProductsContext context)
    {
        _context = context;
    }

    public async Task DeleteProduct(string id, CancellationToken ct = default)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Remove<Product>(product);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async IAsyncEnumerable<Product> GetAllProducts([EnumeratorCancellation] CancellationToken ct = default)
    {
        var products = _context.Products.GetAsyncEnumerator();
        while(await products.MoveNextAsync())
        {
            yield return products.Current;
        }
    }

    public async Task<Product?> GetProduct(string id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task<bool> ProductExists(string id)
    {
        return await _context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task InsertProduct(Product product, CancellationToken ct = default)
    {
        await _context.AddAsync<Product>(product, ct);
        await _context.SaveChangesAsync(ct);
    }
    
    public async Task UpdateProduct(Product product, CancellationToken ct = default)
    {
        _context.Update<Product>(product);
        await _context.SaveChangesAsync(ct);
    }
}

