using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VattaAppApi.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string SellerId { get; set; }
    public string ProductName { get; set; }
    public string Currency { get; set; }
    public double Price { get; set; }
    public double OriginalPrice { get; set; }
    public string Description { get; set; }
    public string CategoryId { get; set; }
    public string Brand { get; set; }
    public string ModelNumber { get; set; }
    public string ItemWeight { get; set; }
    public string Condition { get; set; } 
    public string[] ImagesUrls { get; set; }
    public string ReceiptImageUrl { get; set; }
    public int LikesCount { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime LastUpdatedDate { get; set; }
    public DateTime DeletedDate { get; set; } = new DateTime();
    public bool Sold { get; set; } = false;
    public string BuyerId { get; set; }
    public string[] LoggedInWatchersIds { get; set; }
    public int ViewsCount { get; set; }
    public string ShippingType { get; set; }
    public string PickingAddress { get; set; }

}