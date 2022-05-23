namespace VattaAppApi.Models.DbSettings
{
    public class ProductsDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DbName { get; set; } = null!;

        public string ProductsCollectionName { get; set; } = null!;
        public string OrderedProductsCollectionName { get; set; } = null!;
    }
}
