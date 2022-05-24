namespace VattaAppApi.Models.DbSettings;

public class CustomersDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string CustomersCollectionName { get; set; } = null!;
}