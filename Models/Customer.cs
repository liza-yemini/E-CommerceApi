using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class Customer
{
    [BsonId]
    public string Id { get; set; } //email
    public string Password { get; set; }
    [BsonIgnoreIfNull]
    public string Name { get; set; }
    [BsonIgnoreIfNull]
    public Address Address { get; set; }
    [BsonIgnoreIfDefault]
    public DateTime Birthday { get; set; }
    [BsonIgnoreIfDefault]
    public int Age { get; set; }
    [BsonIgnoreIfNull]
    public string Gender { get; set; }
    [BsonIgnoreIfNull]
    public string[] ViewedProductsIds { get; set; }
    [BsonIgnoreIfNull]
    public string[] LikedProductsIds { get; set; }
    [BsonIgnoreIfNull]
    public string[] FollowedSellersIds { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [BsonIgnoreIfDefault]
    public DateTime LastActiveDate { get; set; }
}