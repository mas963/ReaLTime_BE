using Microsoft.AspNetCore.Mvc;
using ReaLTime.Application.Features.Devices.Commands;
using Wolverine;

namespace ReaLTime.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController: ControllerBase
{
    private readonly IMessageBus _messageBus;
    
    public DevicesController(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterDevice([FromBody] RegisterDeviceCommand command)
    {
        var deviceId = await _messageBus.InvokeAsync<string>(command);
        return Ok(deviceId);
    }
}