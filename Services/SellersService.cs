using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class SellersService
{
    private readonly IMongoCollection<Seller> _sellersCollection;

    public SellersService(IOptions<SellersDbSettings> sellersDbSettings)
    {
        var mongoClient = new MongoClient(
            sellersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            sellersDbSettings.Value.DbName);

        _sellersCollection = mongoDatabase.GetCollection<Seller>(
            sellersDbSettings.Value.SellersCollectionName);
    }

    public async Task<List<Seller>> GetAsync() =>
        await _sellersCollection.Find(_ => true).ToListAsync();

    public async Task<Seller?> GetAsync(string id) =>
        await _sellersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Seller newSeller) =>
        await _sellersCollection.InsertOneAsync(newSeller);

    public async Task UpdateAsync(string id, Seller updateSeller) =>
        await _sellersCollection.ReplaceOneAsync(x => x.Id == id, updateSeller);

    public async Task RemoveAsync(string id) =>
        await _sellersCollection.DeleteOneAsync(x => x.Id == id);
}