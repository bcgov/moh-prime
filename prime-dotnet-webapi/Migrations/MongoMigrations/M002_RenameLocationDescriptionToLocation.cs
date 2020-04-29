using Mongo.Migration.Migrations;
using MongoDB.Bson;
using Prime.Models.MongoModels;

namespace Prime.Migrations
{
    public class M002_RenameLocationDescriptionToLocation : Migration<Locations>
    {
        public M002_RenameLocationDescriptionToLocation()
            : base("0.0.2")
        {
        }

        public override void Up(BsonDocument document)
        {
            var doors = document["LocationDescription"].ToString();
            document.Add("Description", doors);
            document.Remove("LocationDescription");
        }

        public override void Down(BsonDocument document)
        {
            var doors = document["Description"].ToString();
            document.Add("LocationDescription", doors);
            document.Remove("Description");
        }
    }
}
