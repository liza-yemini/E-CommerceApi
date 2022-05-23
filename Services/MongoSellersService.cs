using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoSellersService: ISellersService
{
    private readonly IMongoCollection<Seller> _sellersCollection;

    public MongoSellersService(IOptions<SellersDbSettings> sellersDbSettings)
    {
        var mongoClient = new MongoClient(
            sellersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            sellersDbSettings.Value.DbName);

        _sellersCollection = mongoDatabase.GetCollection<Seller>(
            sellersDbSettings.Value.SellersCollectionName);
    }

    public async Task<List<Seller>> Get() =>
        await _sellersCollection.Find(_ => true).ToListAsync();

    public async Task<Seller?> Get(string id) =>
        await _sellersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Seller newSeller)
    {
        newSeller.Password = BCrypt.Net.BCrypt.HashPassword(newSeller.Password);
        await _sellersCollection.InsertOneAsync(newSeller);
    }

    public async Task Update(string id, Seller updateSeller) =>
        await _sellersCollection.ReplaceOneAsync(x => x.Id == id, updateSeller);

    public async Task Remove(string id) =>
        await _sellersCollection.DeleteOneAsync(x => x.Id == id);

    public async Task Login(string id, string password)
    {
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
        await _sellersCollection.Find(x => x.Id == id && x.Password == hashedPassword).CountDocumentsAsync();
    }
}