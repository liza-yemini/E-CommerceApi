using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class CategoriesService
{
    private readonly IMongoCollection<Category> _categoriesCollection;

    public CategoriesService(IOptions<CategoriesDbSettings> categoriesDbSettings)
    {
        var mongoClient = new MongoClient(
            categoriesDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            categoriesDbSettings.Value.DbName);

        _categoriesCollection = mongoDatabase.GetCollection<Category>(
            categoriesDbSettings.Value.CategoriesCollectionName);
    }

    public async Task<List<Category>> GetAsync() =>
        await _categoriesCollection.Find(_ => true).ToListAsync();

    public async Task<Category?> GetAsync(string id) =>
        await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Category newCategory) =>
        await _categoriesCollection.InsertOneAsync(newCategory);

    public async Task UpdateAsync(string id, Category updateCategory) =>
        await _categoriesCollection.ReplaceOneAsync(x => x.Id == id, updateCategory);

    public async Task RemoveAsync(string id) =>
        await _categoriesCollection.DeleteOneAsync(x => x.Id == id);
}