using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface IProductsService
{
    public Task<List<Product>> Get();
    public Task<Product?> Get(string id);
    public Task<List<Product>> GetByCategory(string category);
    public Task Create(Product newOrderedProduct);
    public Task Update(string id, Product updateOrderedProduct);
    public Task Remove(string id);
}