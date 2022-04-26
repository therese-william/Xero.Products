namespace Xero.Products.BusinessLogic.Models;
public class Product
{
    public Guid Id { get; init; }

    [Required]
    public string Name { get; set; }

    public string? Description { get; init; }

    [Required]
    public decimal Price { get; set; }

    public decimal DeliveryPrice { get; set; }

    public Product()
    {
        Id = Guid.NewGuid();
    }
    public Product(Guid id)
    {
        Id = id;
    }
}
