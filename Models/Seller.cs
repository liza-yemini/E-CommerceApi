﻿using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VattaAppApi.Models;

public class Seller
{
    [BsonId]
    [EmailAddress(ErrorMessage = "Invalid Email")]
    public string Id { get; set; } //email
    [BsonRequiredAttribute]
    public string Password { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
    public DateTime Birthday { get; set; }
    public int Age { get; set; } 
    public string Gender { get; set; }
    public string StoreName { get; set; }
    public string SellerType { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime LastActiveDate { get; set; }

}