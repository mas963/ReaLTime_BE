using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReaLTime.Domain.Entities;

public class Device
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string DeviceToken { get; set; }
    
    public string DeviceType { get; set; }
    
    public string NotificationProvider { get; set; }

    public int ErrorCount { get; set; } = 0;
    
    public DateTime RegisteredAt { get; set; }
    
    public DateTime? LastActiveAt { get; set; }
}

public enum DeviceType
{
    Android,
    Ios,
    Browser
}

public enum NotificationProvider
{
    Fcm,
    OneSignal
}