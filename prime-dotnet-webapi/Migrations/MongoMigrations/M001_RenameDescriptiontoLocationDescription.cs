using Mongo.Migration.Migrations;
using MongoDB.Bson;
using Prime.Models;

namespace Prime.Migrations
{
    public class M001_RenameDescriptiontoLocationDescription : Migration<Locations>
    {
        public M001_RenameDescriptiontoLocationDescription()
            : base("0.0.1")
        {
        }

        public override void Up(BsonDocument document)
        {
            var doors = document["Description"].ToString();
            document.Add("LocationDescription", doors);
            document.Remove("Descritpion");
        }

        public override void Down(BsonDocument document)
        {
            var doors = document["LocationDescription"].ToString();
            document.Add("Description", doors);
            document.Remove("LocationDescription");
        }
    }
}
