namespace Infrastructure.Persistence.Mongo.Configurations
{
    public class MongoDbConfiguration
    {
        public string ConnectionUri { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
    }
}
