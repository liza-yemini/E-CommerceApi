namespace VattaAppApi.Models.DbSettings;

public class CartsDbSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DbName { get; set; } = null!;

    public string CartsCollectionName { get; set; } = null!;
}