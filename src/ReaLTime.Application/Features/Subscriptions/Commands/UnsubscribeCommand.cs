namespace ReaLTime.Application.Features.Subscriptions.Commands;

public class UnsubscribeCommand
{
    public string DeviceId { get; set; }
    public string CreatorId { get; set; }
}