using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface ISellersService
{
    public Task<List<Seller>> Get();
    public Task<Seller?> Get(string id);
    public Task Create(Seller newSeller);
    public Task Update(string id, Seller updateSeller);
    public Task Remove(string id);
    public Task Login(string username, string password);
}