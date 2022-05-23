using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BCrypt.Net;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoBuyersService: IBuyersService
{
    private readonly IMongoCollection<Buyer> _buyersCollection;

    public MongoBuyersService(IOptions<BuyersDbSettings> buyersDbSettings)
    {
        var mongoClient = new MongoClient(
            buyersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            buyersDbSettings.Value.DbName);

        _buyersCollection = mongoDatabase.GetCollection<Buyer>(
            buyersDbSettings.Value.BuyersCollectionName);
    }

    public async Task<List<Buyer>> Get() =>
        await _buyersCollection.Find(_ => true).ToListAsync();

    public async Task<Buyer?> Get(string id) =>
        await _buyersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Buyer newBuyer)
    {
        newBuyer.Password = BCrypt.Net.BCrypt.HashPassword(newBuyer.Password);
        await _buyersCollection.InsertOneAsync(newBuyer);
    }

    public async Task Update(string id, Buyer updateBuyer) =>
        await _buyersCollection.ReplaceOneAsync(x => x.Id == id, updateBuyer);

    public async Task Remove(string id) =>
        await _buyersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task Login(string username, string password)
    {
        throw new NotImplementedException();
    }
}
