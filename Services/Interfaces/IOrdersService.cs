using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface IOrdersService
{
    public Task<List<Order>> Get();
    public Task<Order?> Get(string id);
    public Task Create(Order newOrderedOrder);
    public Task Update(string id, Order updateOrderedOrder);
    public Task Remove(string id);
}