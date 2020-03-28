using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Prime.Models
{
    public class Practitioner
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
