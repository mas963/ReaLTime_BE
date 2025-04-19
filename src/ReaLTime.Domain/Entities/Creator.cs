using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReaLTime.Domain.Entities;

public class Creator
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string ProfileUrl { get; set; }

    public string ImageUrl { get; set; }

    public int SubscriberCount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
