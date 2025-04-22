namespace ReaLTime.Shared.Events;

public class NotificationCreatedEvent
{
    public string NotificationId { get; set; }
    public string CreatorId { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public string? Icon { get; set; }
    public Dictionary<string, string>? Data { get; set; }
}