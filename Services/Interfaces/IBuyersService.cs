using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface IBuyersService
{
    //Get all
    public Task<List<Buyer>> Get();
    //Get one by id
    public Task<Buyer?> Get(string id);
    //Update by id
    public Task Update(string id, Buyer updateBuyer);
    public Task Create(Buyer newBuyer);
    public Task Remove(string id);
    public Task Login(string username, string password);

}