using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

public class Buyer
{
    [BsonId]
    public string Id { get; set; } //email
    public string Password { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public DateTime Birthday { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string[] ViewedProductsIds { get; set; }
    public string[] LikedProductsIds { get; set; }
    public string[] FollowedSellersIds { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime LastActiveDate { get; set; }
}