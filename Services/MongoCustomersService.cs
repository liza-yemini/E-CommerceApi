using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BCrypt.Net;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoCustomersService : ICustomersService
{
    private readonly IMongoCollection<Customer> _customersCollection;

    public MongoCustomersService(IOptions<CustomersDbSettings> customersDbSettings)
    {
        var mongoClient = new MongoClient(
            customersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            customersDbSettings.Value.DbName);

        _customersCollection = mongoDatabase.GetCollection<Customer>(
            customersDbSettings.Value.CustomersCollectionName);
    }

    public async Task<List<Customer>> Get() =>
        await _customersCollection.Find(_ => true).ToListAsync();

    public async Task<Customer?> Get(string id) =>
        await _customersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Customer newCustomer)
    {
        newCustomer.Password = BCrypt.Net.BCrypt.HashPassword(newCustomer.Password);
        await _customersCollection.InsertOneAsync(newCustomer);
    }

    public async Task Update(string id, Customer updateCustomer) =>
        await _customersCollection.ReplaceOneAsync(x => x.Id == id, updateCustomer);

    public async Task Remove(string id) =>
        await _customersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task Login(string username, string password)
    {
        throw new NotImplementedException();
    }
}
