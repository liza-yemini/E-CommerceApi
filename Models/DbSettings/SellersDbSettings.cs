namespace VattaAppApi.Models.DbSettings;

public class SellersDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string SellersCollectionName { get; set; } = null!;
}