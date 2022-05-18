namespace VattaAppApi.Models.DbSettings
{
    public class CategoriesDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DbName { get; set; } = null!;

        public string CategoriesCollectionName { get; set; } = null!;
    }
}
