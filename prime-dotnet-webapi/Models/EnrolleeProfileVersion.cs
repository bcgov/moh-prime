using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mongo.Migration.Documents;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Prime.Models
{
    [Table("EnrolleeProfileVersion")]
    public class EnrolleeProfileVersion : BaseAuditable
    {
        [BsonId]
        [Key]
        public int Id { get; set; }

        public int EnrolleeId { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public Enrollee Enrollee { get; set; }

        [Required]
        [BsonIgnore]
        public JObject ProfileSnapshot { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        // MONGO ATTRIBUTES

        [NotMapped]
        [JsonIgnore]
        public BsonDocument ProfileSnapshotMongo { get; set; }

        [NotMapped]
        [JsonIgnore]
        public DocumentVersion Version { get; set; }
    }
}
