using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductsService(IOptions<ProductsDbSettings> productsDbSettings)
        {
            var mongoClient = new MongoClient(
                productsDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                productsDbSettings.Value.DbName);

            _productsCollection = mongoDatabase.GetCollection<Product>(
                productsDbSettings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> GetAsync() =>
            await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> GetAsync(string id) =>
            await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Product newProduct) =>
            await _productsCollection.InsertOneAsync(newProduct);

        public async Task UpdateAsync(string id, Product updateProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, updateProduct);

        public async Task RemoveAsync(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
