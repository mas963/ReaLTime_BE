namespace ReaLTime.Shared.DTOs;

public class NotificationDto
{
    public string Title { get; set; }
    public string? Body { get; set; }
    public string? Icon { get; set; }
    public Dictionary<string, string>? Data { get; set; }
}