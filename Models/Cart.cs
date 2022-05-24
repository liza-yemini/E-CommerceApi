using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class Cart
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string CustomerId { get; set; }
    [BsonIgnoreIfNull]
    public string[] ProductsIds { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [BsonIgnoreIfDefault]
    public DateTime LastUpdatedDate { get; set; }
    [BsonIgnoreIfDefault]
    public DateTime DeletedDate { get; set; } = new DateTime();
}