using Microsoft.AspNetCore.Mvc;
using NomuBackend.Services;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> Get() =>
        Ok(await _productService.GetProductsAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> Get(string id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Product product)
    {
        await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Product product)
    {
        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct == null) return NotFound();
        await _productService.UpdateProductAsync(id, product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingProduct = await _productService.GetProductByIdAsync(id);
        if (existingProduct == null) return NotFound();
        await _productService.RemoveProductAsync(id);
        return NoContent();
    }
}
