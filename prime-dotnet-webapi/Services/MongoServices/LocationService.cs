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
        private readonly MongoDbContext _mongoContext;

        public LocationService(MongoDbContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<List<Locations>> Get() =>
            await _mongoContext.locations.Find(location => true).ToListAsync();

        public async Task<Locations> Get(string id) =>
            await _mongoContext.locations.Find<Locations>(location => location.Id == id).FirstOrDefaultAsync();

        public async Task<Locations> Create(Locations location)
        {
            await _mongoContext.locations.InsertOneAsync(location);
            return location;
        }

        public async void Update(string id, Locations locationIn) =>
            await _mongoContext.locations.ReplaceOneAsync(location => location.Id == id, locationIn);

        public async void Remove(Locations locationIn) =>
            await _mongoContext.locations.DeleteOneAsync(location => location.Id == locationIn.Id);

        public async void Remove(string id) =>
            await _mongoContext.locations.DeleteOneAsync(location => location.Id == id);
    }
}
