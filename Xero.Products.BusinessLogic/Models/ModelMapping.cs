namespace Xero.Products.BusinessLogic.Models;
public static class ModelMapping
{
    public static Product ToModel(this Entities.Product product)
    {
        return new Product
        {
            Id = Guid.Parse(product.Id),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price ?? 0,
            DeliveryPrice = product.DeliveryPrice??0
        };
    }
    public static Entities.Product ToEntity(this Product product)
    {
        return new Entities.Product
        {
            Id= product.Id.ToString().ToUpper(),
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            DeliveryPrice = product.DeliveryPrice
        };
    }
    public static ProductOption ToModel(this Entities.ProductOption productOption)
    {
        return new ProductOption
        {
            Id = Guid.Parse(productOption.Id),
            ProductId = Guid.Parse(productOption.ProductId),
            Name = productOption.Name,
            Description = productOption.Description
        };
    }
    public static Entities.ProductOption ToEntity(this ProductOption productOption)
    {
        return new Entities.ProductOption
        {
            Id = productOption.Id.ToString().ToUpper(),
            ProductId = productOption.ProductId.ToString(),
            Name = productOption.Name,
            Description = productOption.Description
        };
    }
}