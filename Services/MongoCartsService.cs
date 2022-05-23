using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoCartsService: ICartsService
{
    private readonly IMongoCollection<Cart> _cartsCollection;

    public MongoCartsService(IOptions<CartsDbSettings> cartsDbSettings)
    {
        var mongoClient = new MongoClient(
            cartsDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            cartsDbSettings.Value.DbName);

        _cartsCollection = mongoDatabase.GetCollection<Cart>(
            cartsDbSettings.Value.CartsCollectionName);
    }

    public async Task<List<Cart>> Get() =>
        await _cartsCollection.Find(_ => true).ToListAsync();

    public async Task<Cart?> Get(string id) =>
        await _cartsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Cart newCart) =>
        await _cartsCollection.InsertOneAsync(newCart);

    public async Task Update(string id, Cart updateCart) =>
        await _cartsCollection.ReplaceOneAsync(x => x.Id == id, updateCart);

    public async Task Remove(string id) =>
        await _cartsCollection.DeleteOneAsync(x => x.Id == id);
}