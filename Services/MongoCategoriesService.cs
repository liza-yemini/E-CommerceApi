using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;
using VattaAppApi.Services.Interfaces;

namespace VattaAppApi.Services;

public class MongoCategoriesService: ICategoriesService
{
    private readonly IMongoCollection<Category> _categoriesCollection;

    public MongoCategoriesService(IOptions<CategoriesDbSettings> categoriesDbSettings)
    {
        var mongoClient = new MongoClient(
            categoriesDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            categoriesDbSettings.Value.DbName);

        _categoriesCollection = mongoDatabase.GetCollection<Category>(
            categoriesDbSettings.Value.CategoriesCollectionName);
    }

    public async Task<List<Category>> Get() =>
        await _categoriesCollection.Find(_ => true).ToListAsync();

    public async Task<Category?> Get(string id) =>
        await _categoriesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task Create(Category newCategory) =>
        await _categoriesCollection.InsertOneAsync(newCategory);

    public async Task Update(string id, Category updateCategory) =>
        await _categoriesCollection.ReplaceOneAsync(x => x.Id == id, updateCategory);

    public async Task Remove(string id) =>
        await _categoriesCollection.DeleteOneAsync(x => x.Id == id);
}