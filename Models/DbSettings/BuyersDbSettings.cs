namespace VattaAppApi.Models.DbSettings;

public class BuyersDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string BuyersCollectionName { get; set; } = null!;
}