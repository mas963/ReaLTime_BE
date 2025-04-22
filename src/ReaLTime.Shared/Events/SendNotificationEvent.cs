namespace ReaLTime.Shared.Events;

public class SendNotificationEvent
{
    public string NotificationId { get; set; }
    public string DeviceId { get; set; }
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public string? Icon { get; set; }
    public Dictionary<string, string>? Data { get; set; }
}