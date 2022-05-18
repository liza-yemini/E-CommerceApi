using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VattaAppApi.Models;
using VattaAppApi.Models.DbSettings;

namespace VattaAppApi.Services;

public class ClientsService
{
    private readonly IMongoCollection<Client> _clientsCollection;

    public ClientsService(IOptions<ClientsDbSettings> clientsDbSettings)
    {
        var mongoClient = new MongoClient(
            clientsDbSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            clientsDbSettings.Value.DbName);

        _clientsCollection = mongoDatabase.GetCollection<Client>(
            clientsDbSettings.Value.ClientsCollectionName);
    }

    public async Task<List<Client>> GetAsync() =>
        await _clientsCollection.Find(_ => true).ToListAsync();

    public async Task<Client?> GetAsync(string id) =>
        await _clientsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Client newClient) =>
        await _clientsCollection.InsertOneAsync(newClient);

    public async Task UpdateAsync(string id, Client updateClient) =>
        await _clientsCollection.ReplaceOneAsync(x => x.Id == id, updateClient);

    public async Task RemoveAsync(string id) =>
        await _clientsCollection.DeleteOneAsync(x => x.Id == id);
}
