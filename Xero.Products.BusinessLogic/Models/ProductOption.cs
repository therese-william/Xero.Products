namespace Xero.Products.BusinessLogic.Models;

public class ProductOption
{
    public Guid Id { get; init; }
    [Required]
    public Guid ProductId { get; set; }
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }

    public ProductOption()
    {
        Id = Guid.NewGuid();
    }
    public ProductOption(Guid id)
    {
        Id=id;
    }
}
