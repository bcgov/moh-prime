using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Prime.Models;

namespace Prime.Services
{
    public class EnrolleeProfileVersionService : BaseService, IEnrolleeProfileVersionService
    {
        private readonly IMongoCollection<EnrolleeProfileVersion> _profileVersions;

        private JsonSerializer _camelCaseSerializer = JsonSerializer.Create(
            new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
        );

        public EnrolleeProfileVersionService(
            ApiDbContext context,
            IHttpContextAccessor httpContext,
            IMongoDbSettings settings
            ) : base(context, httpContext)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _profileVersions = database.GetCollection<EnrolleeProfileVersion>("EnrolleeProfileVersions");
        }

        public async Task<IEnumerable<EnrolleeProfileVersion>> GetEnrolleeProfileVersionsAsync(int enrolleeId)
        {
            return await _context.EnrolleeProfileVersions
                .Where(epv => epv.EnrolleeId == enrolleeId)
                .ToListAsync();
        }

        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionAsync(int enrolleeProfileVersionId)
        {
            return await _context.EnrolleeProfileVersions
                .SingleOrDefaultAsync(epv => epv.Id == enrolleeProfileVersionId);
        }

        /**
          * Get the most recent Profile version before a given time.
          */
        public async Task<EnrolleeProfileVersion> GetEnrolleeProfileVersionBeforeDateAsync(int enrolleeId, DateTimeOffset dateTime)
        {
            return await _context.EnrolleeProfileVersions
            .Where(epv => epv.EnrolleeId == enrolleeId)
            .Where(epv => epv.CreatedDate < dateTime)
            .OrderByDescending(epv => epv.CreatedDate)
            .FirstOrDefaultAsync();
        }

        public async Task CreateEnrolleeProfileVersionAsync(Enrollee enrollee)
        {
            var enrolleeProfileVersion = new EnrolleeProfileVersion
            {
                EnrolleeId = enrollee.Id,
                ProfileSnapshot = JObject.FromObject(enrollee, _camelCaseSerializer),
                CreatedDate = DateTimeOffset.Now
            };

            _context.EnrolleeProfileVersions.Add(enrolleeProfileVersion);

            await _context.SaveChangesAsync();

            enrolleeProfileVersion.ProfileSnapshotMongo = BsonDocument.Parse(enrolleeProfileVersion.ProfileSnapshot.ToString());

            // Insert into mongo database
            _profileVersions.InsertOne(enrolleeProfileVersion);
        }
    }
}
