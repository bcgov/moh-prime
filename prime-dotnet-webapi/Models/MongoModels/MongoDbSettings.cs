namespace Prime.Models
{
    public class MongoDbSettings : IMongoDbSettings
    {
        public string MongoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }

    public interface IMongoDbSettings
    {
        string MongoCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
