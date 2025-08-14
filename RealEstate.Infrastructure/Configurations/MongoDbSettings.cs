namespace RealEstate.Infrastructure.Configurations
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string PropertiesCollectionName { get; set; } = "properties";
        public string OwnersCollectionName { get; set; } = "owners";
        public string PropertyImagesCollectionName { get; set; } = "propertyimages";
        public string PropertyTracesCollectionName { get; set; } = "propertytraces";
    }
}
