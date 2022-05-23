using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services
{
    public class MongoProductsService: IProductsService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public MongoProductsService(IOptions<ProductsDbSettings> productsDbSettings)
        {
            var mongoClient = new MongoClient(
                productsDbSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                productsDbSettings.Value.DbName);

            _productsCollection = mongoDatabase.GetCollection<Product>(
                productsDbSettings.Value.ProductsCollectionName);
        }

        public async Task<List<Product>> Get() =>
            await _productsCollection.Find(_ => true).ToListAsync();

        public async Task<Product?> Get(string id) =>
            await _productsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<List<Product>> GetByCategory(string category) =>
            await _productsCollection.Find(x => x.CategoriesIds.Contains(category)).ToListAsync();
        public async Task Create(Product newProduct) =>
            await _productsCollection.InsertOneAsync(newProduct);

        public async Task Update(string id, Product updateProduct) =>
            await _productsCollection.ReplaceOneAsync(x => x.Id == id, updateProduct);

        public async Task Remove(string id) =>
            await _productsCollection.DeleteOneAsync(x => x.Id == id);
    }
}
