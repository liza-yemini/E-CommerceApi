using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class MongoOrderedProductsService
{
    private readonly IMongoCollection<OrderedProduct> _orderedProdcutsCollection;

    public MongoOrderedProductsService(IOptions<ProductsDbSettings> productsDbSettings)
    {
        var mongoClient = new MongoClient(
            productsDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            productsDbSettings.Value.DbName);

        _orderedProdcutsCollection = mongoDatabase.GetCollection<OrderedProduct>(
            productsDbSettings.Value.OrderedProductsCollectionName);
    }

    public async Task<List<OrderedProduct>> Get() =>
        await _orderedProdcutsCollection.Find(_ => true).ToListAsync();

    public async Task<OrderedProduct?> Get(string id) =>
        await _orderedProdcutsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(OrderedProduct newOrderedProduct) =>
        await _orderedProdcutsCollection.InsertOneAsync(newOrderedProduct);

    public async Task Update(string id, OrderedProduct updateOrderedProduct) =>
        await _orderedProdcutsCollection.ReplaceOneAsync(x => x.Id == id, updateOrderedProduct);

    public async Task Remove(string id) =>
        await _orderedProdcutsCollection.DeleteOneAsync(x => x.Id == id);
}