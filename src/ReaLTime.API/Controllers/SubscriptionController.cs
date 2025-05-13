using Microsoft.AspNetCore.Mvc;
using ReaLTime.Application.Features.Subscriptions.Commands;
using Wolverine;

namespace ReaLTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController: ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public SubscriptionController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    [HttpPost("subscribe")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeCommand command)
    {
        var result = await _messageBus.InvokeAsync<bool>(command);
        return Ok(result);
    }
    
    [HttpPost("unsubscribe")]
    public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeCommand command)
    {
        var result = await _messageBus.InvokeAsync<bool>(command);
        return Ok(result);
    }
}