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

    public string DeviceId { get; set; }

    public DeviceType DeviceType { get; set; }

    public string PushToken { get; set; }

    public string BrowserEndpoint { get; set; }

    public string P256dh { get; set; } // for browser

    public string Auth { get; set; } // for browser

    public bool IsActive { get; set; } = true;

    public DateTime? LastSuccessfulNotificationAt { get; set; }

    public int FailureCount { get; set; } = 0;

    public DateTime CreatedDate { get; set; }
}

public enum DeviceType
{
    Android,
    Ios,
    Browser
}
