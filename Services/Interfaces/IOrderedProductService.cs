using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface IOrderedProductService
{
    public Task<List<OrderedProduct>> Get();
    public Task<OrderedProduct?> Get(string id);
    public Task Create(OrderedProduct newOrderedProduct);
    public Task Update(string id, OrderedProduct updateOrderedProduct);
    public Task Remove(string id);
}