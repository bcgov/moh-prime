using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Prime.Models;
using Prime.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
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
            // System.Console.WriteLine("MONGO_CONNECTION_STRING: ", System.Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING"));
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
