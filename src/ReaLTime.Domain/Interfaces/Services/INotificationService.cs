using ReaLTime.Shared.DTOs;

namespace ReaLTime.Domain.Interfaces.Services;

public interface INotificationService
{
    Task <bool> SendNotificationAsync(NotificationDto notificationDto, DeviceDto deviceDto);
    // Task <bool> CanHandleDeviceType(DeviceType deviceType);
    string GetProviderName();
}