using ProductManagement.Domain;

namespace ProductManagement.Application;

public class UpdateProductUseCase
{
    private readonly IProductRepository _repository;

    public UpdateProductUseCase(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(Product product)
    {
        // Có thể thêm validation: product tồn tại không?
        await _repository.UpdateProductAsync(product);
    }
}
