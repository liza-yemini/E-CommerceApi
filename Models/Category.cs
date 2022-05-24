using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class Category
{
    [BsonId]
    public string Id { get; set; } //Name
    [BsonIgnoreIfNull]
    public string ParentId { get; set; } //Parent Name
}