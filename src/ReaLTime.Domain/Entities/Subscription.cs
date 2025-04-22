using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ReaLTime.Domain.Entities;

public class Subscription
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CreatorId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string DeviceId { get; set; }
    
    public DateTime SubscribedAt { get; set; }
    
    public bool IsActive { get; set; } = true;
}