using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prime.Models;
using Prime.Services;

namespace Prime.Services
{
    public class LocationService : ILocationService
    {
        private readonly IMongoCollection<Locations> _locations;

        public LocationService(IMongoDbSettings settings)
        {
            var connectionString = System.Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING");
            if (connectionString == null)
            {
                connectionString = settings.ConnectionString;
            }

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _locations = database.GetCollection<Locations>(settings.MongoCollectionName);
        }

        public async Task<List<Locations>> Get() =>
            await _locations.Find(location => true).ToListAsync();

        public async Task<Locations> Get(string id) =>
            await _locations.Find<Locations>(location => location.Id == id).FirstOrDefaultAsync();

        public async Task<Locations> Create(Locations location)
        {
            await _locations.InsertOneAsync(location);
            return location;
        }

        public async void Update(string id, Locations locationIn) =>
            await _locations.ReplaceOneAsync(location => location.Id == id, locationIn);

        public async void Remove(Locations locationIn) =>
            await _locations.DeleteOneAsync(location => location.Id == locationIn.Id);

        public async void Remove(string id) =>
            await _locations.DeleteOneAsync(location => location.Id == id);
    }
}
