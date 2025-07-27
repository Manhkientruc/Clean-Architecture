using ProductManagement.Domain;

namespace ProductManagement.Application;

public interface IProductRepository
{
    Task AddProductAsync(Product product);
    Task<Product?> GetProductByIdAsync(Guid id);
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
}
