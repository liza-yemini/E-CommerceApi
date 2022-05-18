using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class CartsService
{
    private readonly IMongoCollection<Cart> _cartsCollection;

    public CartsService(IOptions<CartsDbSettings> cartsDbSettings)
    {
        var mongoClient = new MongoClient(
            cartsDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            cartsDbSettings.Value.DbName);

        _cartsCollection = mongoDatabase.GetCollection<Cart>(
            cartsDbSettings.Value.CartsCollectionName);
    }

    public async Task<List<Cart>> GetAsync() =>
        await _cartsCollection.Find(_ => true).ToListAsync();

    public async Task<Cart?> GetAsync(string id) =>
        await _cartsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Cart newCart) =>
        await _cartsCollection.InsertOneAsync(newCart);

    public async Task UpdateAsync(string id, Cart updateCart) =>
        await _cartsCollection.ReplaceOneAsync(x => x.Id == id, updateCart);

    public async Task RemoveAsync(string id) =>
        await _cartsCollection.DeleteOneAsync(x => x.Id == id);
}