using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class OrdersService
{
    private readonly IMongoCollection<Product> _ordersCollection;

    public OrdersService(IOptions<OrdersDbSettings> ordersDbSettings)
    {
        var mongoClient = new MongoClient(
            ordersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            ordersDbSettings.Value.DbName);

        _ordersCollection = mongoDatabase.GetCollection<Product>(
            ordersDbSettings.Value.OrdersCollectionName);
    }

    public async Task<List<Product>> GetAsync() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Product?> GetAsync(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Product newProduct) =>
        await _ordersCollection.InsertOneAsync(newProduct);

    public async Task UpdateAsync(string id, Product updateProduct) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updateProduct);

    public async Task RemoveAsync(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);
}