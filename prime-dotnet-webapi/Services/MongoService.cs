using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Prime.Models;
using Prime.Services;

namespace Prime.Services
{
    public class MongoService : IMongoService
    {
        private readonly IMongoCollection<Locations> _locations;

        public MongoService(IMongoDbSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _locations = database.GetCollection<Locations>(settings.MongoCollectionName);
        }

        public List<Locations> Get() =>
            _locations.Find(location => true).ToList();

        public Locations Get(string id) =>
            _locations.Find<Locations>(location => location.Id == id).FirstOrDefault();

        public Locations Create(Locations location)
        {
            _locations.InsertOne(location);
            return location;
        }

        public void Update(string id, Locations locationIn) =>
            _locations.ReplaceOne(location => location.Id == id, locationIn);

        public void Remove(Locations locationIn) =>
            _locations.DeleteOne(location => location.Id == locationIn.Id);

        public void Remove(string id) =>
            _locations.DeleteOne(location => location.Id == id);

    }
}
