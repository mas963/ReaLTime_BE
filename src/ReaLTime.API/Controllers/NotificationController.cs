using Microsoft.AspNetCore.Mvc;
using ReaLTime.Application.Features.Notifications.Commands;
using Wolverine;

namespace ReaLTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController: ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public NotificationController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationHandler command)
    {
        var notificationId = await _messageBus.InvokeAsync<string>(command);
        return Ok(notificationId);
    }
}