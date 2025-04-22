using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Interfaces.Services;

public interface INotificationService
{
    Task <bool> SendNotificationAsync(Notification notification, Subscription subscription);
    Task <bool> CanHandleDeviceType(DeviceType deviceType);
    string GetProviderName();
}