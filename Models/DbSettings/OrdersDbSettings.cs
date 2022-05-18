namespace VattaAppApi.Models.DbSettings;

public class OrdersDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string OrdersCollectionName { get; set; } = null!;
}