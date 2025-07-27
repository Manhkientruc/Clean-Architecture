using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application;
using ProductManagement.Domain;

namespace ProductManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductsController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        var useCase = new CreateProductUseCase(_repository);
        await useCase.ExecuteAsync(product);
        return Ok(product);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _repository.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _repository.GetAllProductsAsync();
        return Ok(products);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, Product product)
    {
        if (id != product.Id) return BadRequest();

        var useCase = new UpdateProductUseCase(_repository);
        await useCase.ExecuteAsync(product);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var useCase = new DeleteProductUseCase(_repository);
        await useCase.ExecuteAsync(id);

        return NoContent();
    }
}
