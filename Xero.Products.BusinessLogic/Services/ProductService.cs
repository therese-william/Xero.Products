namespace Xero.Products.BusinessLogic.Services;

public class ProductService : IProductService
{
    readonly IProductRepository _productRepository;
    readonly IProductOptionsRepository _productOptionsRepository;

    public ProductService(IProductRepository productRepository, IProductOptionsRepository productOptionsRepository)
    {
        _productRepository = productRepository;
        _productOptionsRepository = productOptionsRepository;
    }

    public async IAsyncEnumerable<Models.Product> GetProducts([EnumeratorCancellation] CancellationToken ct = default)
    {

        var products = _productRepository.GetAllProducts(ct);
        await foreach(var product in products)
        {
            yield return product.ToModel();
        }
    }

    public async Task<Models.Product?> GetProductById(Guid productId)
    {
        var product = await _productRepository.GetProduct(productId.ToString());
        var model = product?.ToModel();
        return model;
    }

    public async Task SaveProduct(Models.Product product, Guid? productId=null, CancellationToken ct = default)
    {
        if(productId.HasValue)
        {
            var entityId = productId.Value.ToString().ToUpper();
            var updatedProduct = await _productRepository.GetProduct(entityId);
            if (updatedProduct != null)
            {
                updatedProduct.Name = product.Name;
                updatedProduct.Description = product.Description;
                updatedProduct.Price = product.Price;
                updatedProduct.DeliveryPrice = product.DeliveryPrice;
                await _productRepository.UpdateProduct(updatedProduct);
            }
        }
        else
        {
            var entity = product.ToEntity();
            if (!await _productRepository.ProductExists(entity.Id))
            {
                await _productRepository.InsertProduct(entity, ct);
            }
        }
    }

    public async Task DeleteProduct(Guid productId, CancellationToken ct = default)
    {
        await _productRepository.DeleteProduct(productId.ToString(), ct);
    }

    public async IAsyncEnumerable<ProductOption> GetProductOptions(Guid productId, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var options = _productOptionsRepository.GetProductOptions(productId.ToString(), ct);
        await foreach(var option in options)
        {
            yield return option.ToModel();
        }
    }

    public async Task<ProductOption?> GetProductOptionById(Guid optionId)
    {
        var entityId = optionId.ToString().ToUpper();
        var productOption = await _productOptionsRepository.GetProductOption(entityId);
        var model = productOption?.ToModel();
        return model;
    }

    public async Task SaveProductOption(ProductOption productOption, Guid? optionId = null, CancellationToken ct = default)
    {
        if(optionId.HasValue)
        {
            var entityId = optionId.Value.ToString().ToUpper();
            var updatedOption = await _productOptionsRepository.GetProductOption(entityId, ct);
            if (updatedOption != null)
            {
                updatedOption.Name = productOption.Name;
                updatedOption.Description = productOption.Description;
                await _productOptionsRepository.UpdateProductOption(updatedOption, ct);
            }
        }
        else
        {
            var entity = productOption.ToEntity();
            if (!await _productOptionsRepository.ProductOptionExists(entity.Id))
            {
                await _productOptionsRepository.InsertProductOption(entity, ct);
            }
        }
    }

    public async Task DeleteProductOption(Guid optionId, CancellationToken ct = default)
    {
        await _productOptionsRepository.DeleteProductOption(optionId.ToString(), ct);
    }
}
