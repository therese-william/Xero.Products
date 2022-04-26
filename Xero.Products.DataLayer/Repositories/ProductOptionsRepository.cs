namespace Xero.Products.DataLayer.Repositories;

public class ProductOptionsRepository : IProductOptionsRepository
{
    readonly ProductsContext _context;

    public ProductOptionsRepository(ProductsContext context)
    {
        _context = context;
    }

    public async IAsyncEnumerable<ProductOption> GetProductOptions(string productId, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var options = _context.ProductOptions.GetAsyncEnumerator();
        while(await options.MoveNextAsync())
        {
            yield return options.Current;
        }
    }

    public async Task<ProductOption?> GetProductOption(string optionId, CancellationToken ct = default)
    {
        return await _context.ProductOptions.FindAsync(optionId, ct);
    }

    public async Task InsertProductOption(ProductOption productOption, CancellationToken ct = default)
    {
        await _context.AddAsync<ProductOption>(productOption,ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task UpdateProductOption(ProductOption productOption, CancellationToken ct = default)
    {
        _context.Update<ProductOption>(productOption);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductOption(string optionId, CancellationToken ct = default)
    {
        var option = await _context.ProductOptions.FindAsync(optionId, ct);
        if (option != null)
        {
            _context.Remove<ProductOption>(option);
            await _context.SaveChangesAsync(ct);
        }
    }

    public async Task<bool> ProductOptionExists(string optionId, CancellationToken ct = default)
    {
        return await _context.ProductOptions.AnyAsync(po => po.Id == optionId, ct);
    }
}

