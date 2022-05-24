using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string CustomerId { get; set; }
    public double TotalProductsPrice { get; set; }
    public double TotalShippingPrice { get; set; }
    public string Currency { get; set; }
    public string[] OrderedProductsIds { get; set; }
}