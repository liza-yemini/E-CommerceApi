namespace VattaAppApi.Models.DbSettings;

public class ClientsDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string ClientsCollectionName { get; set; } = null!;
}