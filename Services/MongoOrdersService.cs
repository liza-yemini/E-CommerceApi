using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoOrdersService: IOrdersService
{
    private readonly IMongoCollection<Order> _ordersCollection;

    public MongoOrdersService(IOptions<OrdersDbSettings> ordersDbSettings)
    {
        var mongoClient = new MongoClient(
            ordersDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            ordersDbSettings.Value.DbName);

        _ordersCollection = mongoDatabase.GetCollection<Order>(
            ordersDbSettings.Value.OrdersCollectionName);
    }

    public async Task<List<Order>> Get() =>
        await _ordersCollection.Find(_ => true).ToListAsync();

    public async Task<Order?> Get(string id) =>
        await _ordersCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Order newOrder) =>
        await _ordersCollection.InsertOneAsync(newOrder);

    public async Task Update(string id, Order updateOrder) =>
        await _ordersCollection.ReplaceOneAsync(x => x.Id == id, updateOrder);

    public async Task Remove(string id) =>
        await _ordersCollection.DeleteOneAsync(x => x.Id == id);
}