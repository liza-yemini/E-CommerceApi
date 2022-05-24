using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class OrderedProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ProductId { get; set; }
    [BsonIgnoreIfNull]
    public string SellerId { get; set; }
    public string CustomerId { get; set; }
    public string Price { get; set; }
    [BsonIgnoreIfNull]
    public string ShippingType { get; set; }
    [BsonIgnoreIfNull]
    public Address ShippingAddress { get; set; }
    [BsonIgnoreIfNull]
    public string ShippingPrice { get; set; }
    [BsonIgnoreIfNull]
    public Address PickupAddress { get; set; }
    public DateTime OrderedDate { get; set; } = DateTime.Now;
    [BsonIgnoreIfDefault]
    public DateTime Delivered { get; set; } = new DateTime();
    [BsonIgnoreIfNull]
    public string TrackingId { get; set; }
    [BsonIgnoreIfNull]
    public string Review { get; set; }
    [BsonIgnoreIfDefault]
    public int Grading { get; set; }
}

