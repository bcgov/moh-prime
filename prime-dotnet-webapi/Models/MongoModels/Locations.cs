using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prime.Models
{
    public class Locations
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
