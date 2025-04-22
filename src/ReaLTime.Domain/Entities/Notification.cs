using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReaLTime.Domain.Entities;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string CreatorId { get; set; }

    public string Title { get; set; }

    public string? Body { get; set; }
    
    public Dictionary<string, string>? Data { get; set; }

    public string? Link { get; set; }

    public string? Icon { get; set; }

    public DateTime CreatedAt { get; set; }
}


