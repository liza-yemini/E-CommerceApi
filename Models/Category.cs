using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

public class Category
{
    [BsonId]
    public string Id { get; set; } //Name
    public string ParentId { get; set; } //Parent Name
}