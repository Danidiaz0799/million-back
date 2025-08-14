namespace RealEstate.Infrastructure.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PropertiesCollectionName { get; set; } = "properties";
    }
}
