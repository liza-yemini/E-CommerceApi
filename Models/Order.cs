using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string BuyerId { get; set; }
    public string TotalProductsPrice { get; set; }
    public string TotalShippingPrice { get; set; }
    public string[] OrderedProductsIds { get; set; }
}