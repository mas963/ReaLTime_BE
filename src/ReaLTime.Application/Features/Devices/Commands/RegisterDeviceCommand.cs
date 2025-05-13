using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Application.Features.Devices.Commands;

public class RegisterDeviceCommand
{
    public string DeviceToken { get; set; }
    public string DeviceType { get; set; }
    public string NotificationProvider { get; set; }
}

public class RegisterDeviceCommandHandler
{
    private readonly IDeviceRepository _deviceRepository;
    
    public RegisterDeviceCommandHandler(IDeviceRepository deviceRepository)
    {
        _deviceRepository = deviceRepository;
    }
    
    public async Task<string> HandleAsync(RegisterDeviceCommand command)
    {
        var existingDevice = await _deviceRepository.GetByTokenAsync(command.DeviceToken);
        
        if (existingDevice != null)
        {
            existingDevice.LastActiveAt = DateTime.UtcNow;
            await _deviceRepository.UpdateAsync(existingDevice);
            return existingDevice.Id;
        }
        
        var device = new Device
        {
            Id = Guid.NewGuid().ToString(),
            DeviceToken = command.DeviceToken,
            DeviceType = command.DeviceType,
            NotificationProvider = command.NotificationProvider,
            RegisteredAt = DateTime.UtcNow,
            LastActiveAt = DateTime.UtcNow,
        };
        
        await _deviceRepository.CreateAsync(device);
        return device.Id;
    }
}