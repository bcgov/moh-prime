using Prime.Models;
using Prime.Models.MongoModels;
using MongoDB.Driver;

namespace Prime
{
    public class MongoDbContext
    {
        private readonly IMongoDbSettings _settings;
        public readonly IMongoCollection<EnrolleeProfileVersion> profileVersions;

        public readonly IMongoCollection<Locations> locations;

        public MongoDbContext(
            IMongoDbSettings settings
            )
        {
            _settings = settings;
            // Connect to database
            var connectionString = System.Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            if (connectionString == null)
            {
                connectionString = settings.ConnectionString;
            }
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            profileVersions = database.GetCollection<EnrolleeProfileVersion>("EnrolleeProfileVersions");

            locations = database.GetCollection<Locations>(settings.MongoCollectionName);
        }


    }
}
