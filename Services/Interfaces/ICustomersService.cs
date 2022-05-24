using VattaAppApi.Models;

namespace VattaAppApi.Services.Interfaces;

public interface ICustomersService
{
    //Get all
    public Task<List<Customer>> Get();
    //Get one by id
    public Task<Customer?> Get(string id);
    //Update by id
    public Task Update(string id, Customer updateCustomer);
    public Task Create(Customer newCustomer);
    public Task Remove(string id);
    public Task Login(string username, string password);

}