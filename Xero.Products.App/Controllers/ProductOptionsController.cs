namespace Xero.Products.App.Controllers;

[Authorize(Roles = "Admin, User")]
[Route("api/Products")]
[ApiController]
public class ProductOptionsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductOptionsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet("{productId}/options")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetOptions(Guid productId)
    {
        var options = await _productService.GetProductOptions(productId).ToListAsync();
        return Ok(options);
    }

    [HttpGet("{productId}/options/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOption(Guid id)
    {
        var option = await _productService.GetProductOptionById(id);
        if (option == null)
        {
            return NotFound($"No option found with id: {id}");
        }
        return Ok(option);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("{productId}/options")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOption(Guid productId, ProductOption option)
    {
        await _productService.SaveProductOption(option);
        return Ok(option);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{productId}/options/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateOption(Guid id, ProductOption option)
    {
        await _productService.SaveProductOption(option, id);
        return Ok();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{productId}/options/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> DeleteOption(Guid id)
    {
        await _productService.DeleteProductOption(id);
        return Ok();
    }
}
