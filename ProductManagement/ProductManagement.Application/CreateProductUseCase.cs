using ProductManagement.Domain;

namespace ProductManagement.Application;

public class CreateProductUseCase
{
    private readonly IProductRepository _repository;

    public CreateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Product product)
    {
        // Validation logic có thể thêm tại đây
        await _repository.AddProductAsync(product);
    }
}
