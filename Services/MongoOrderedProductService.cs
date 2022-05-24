using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoOrderedProductService: IOrderedProductService
{
    private readonly IMongoCollection<OrderedProduct> _orderedProductsCollection;

    public MongoOrderedProductService(IOptions<ProductsDbSettings> productsDbSettings)
    {
        var mongoClient = new MongoClient(
            productsDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            productsDbSettings.Value.DbName);

        _orderedProductsCollection = mongoDatabase.GetCollection<OrderedProduct>(
            productsDbSettings.Value.OrderedProductsCollectionName);
    }

    public async Task<List<OrderedProduct>> Get() =>
        await _orderedProductsCollection.Find(_ => true).ToListAsync();

    public async Task<OrderedProduct?> Get(string id) =>
        await _orderedProductsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(OrderedProduct newOrderedProduct) =>
        await _orderedProductsCollection.InsertOneAsync(newOrderedProduct);

    public async Task Update(string id, OrderedProduct updateOrderedProduct) =>
        await _orderedProductsCollection.ReplaceOneAsync(x => x.Id == id, updateOrderedProduct);

    public async Task Remove(string id) =>
        await _orderedProductsCollection.DeleteOneAsync(x => x.Id == id);
}