using Mongo.Migration.Documents;
using Mongo.Migration.Documents.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prime.Models
{
    [StartUpVersion("0.0.1")]
    [CollectionLocation("Locations", "TestLocations")]
    public class Locations : IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DocumentVersion Version { get; set; }
    }
}
