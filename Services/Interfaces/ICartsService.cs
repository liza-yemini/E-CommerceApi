using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface ICartsService
{
    public Task<List<Cart>> Get();
    public Task<Cart?> Get(string id);
    public Task Create(Cart newCart);
    public Task Update(string id, Cart updateCart);
    public Task Remove(string id);
}