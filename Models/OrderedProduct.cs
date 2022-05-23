using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;
public class OrderedProduct
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string ProductId { get; set; }
    public string SellerId { get; set; }
    public string BuyerId { get; set; }
    public string Price { get; set; }
    public string ShippingType { get; set; }
    public Address ShippingAddress { get; set; }
    public string ShippingPrice { get; set; }
    public Address PickupAddress { get; set; }
    public DateTime OrderedDate { get; set; } = DateTime.Now;
    public DateTime Delivered { get; set; } = new DateTime();
    public string TrackingId { get; set; }
    public string Review { get; set; }
    public int Grading { get; set; }
}

