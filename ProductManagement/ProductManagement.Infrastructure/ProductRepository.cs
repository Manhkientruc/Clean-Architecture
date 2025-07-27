using ProductManagement.Application;
using ProductManagement.Domain;

namespace ProductManagement.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new(); // In-memory storage

    public Task AddProductAsync(Product product)
    {
        _products.Add(product);
        return Task.CompletedTask;
    }

    public Task<Product?> GetProductByIdAsync(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task UpdateProductAsync(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            _products[index] = product;
        }
        return Task.CompletedTask;
    }

    public Task DeleteProductAsync(Guid id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
        return Task.CompletedTask;
    }
}
