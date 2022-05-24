using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace VattaAppApi.Models;

[BsonIgnoreExtraElements]
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string SellerId { get; set; }
    public string ProductName { get; set; }
    [BsonIgnoreIfNull]
    public string Currency { get; set; }
    public double Price { get; set; }
    [BsonIgnoreIfDefault]
    public double OriginalPrice { get; set; }
    [BsonIgnoreIfNull]
    public string Description { get; set; }
    [BsonIgnoreIfNull]
    public string[] CategoriesIds { get; set; }
    [BsonIgnoreIfNull]
    public string Brand { get; set; }
    [BsonIgnoreIfNull]
    public string ModelNumber { get; set; }
    [BsonIgnoreIfNull]
    public string ItemWeight { get; set; }
    [BsonIgnoreIfNull]
    public string Condition { get; set; }
    [BsonIgnoreIfNull]
    public string[] ImagesUrls { get; set; }
    [BsonIgnoreIfNull]
    public string ReceiptImageUrl { get; set; }
    [BsonIgnoreIfDefault]
    public int LikesCount { get; set; } = 0;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    [BsonIgnoreIfDefault]
    public DateTime LastUpdatedDate { get; set; }
    [BsonIgnoreIfDefault]
    public DateTime DeletedDate { get; set; } = new DateTime();
    [BsonIgnoreIfNull]
    public string CustomerId { get; set; }
    [BsonIgnoreIfNull]
    public string[] LoggedInWatchersIds { get; set; }
    public int ViewsCount { get; set; }
    [BsonIgnoreIfNull]
    public string[] ShippingTypes { get; set; }
    [BsonIgnoreIfDefault]
    public double ShippingPrice { get; set; }
    [BsonIgnoreIfNull]
    public Address PickupAddress { get; set; }
    [BsonIgnoreIfNull]
    public bool Sold { get; set; } = false;

}