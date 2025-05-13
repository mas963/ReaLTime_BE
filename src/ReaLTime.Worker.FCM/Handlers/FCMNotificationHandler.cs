using Microsoft.Extensions.Logging;
using ReaLTime.Domain.Interfaces.Repositories;
using ReaLTime.Domain.Interfaces.Services;
using ReaLTime.Shared.DTOs;
using ReaLTime.Shared.Events;

namespace ReaLTime.Worker.FCM.Handlers;

public class FCMNotificationHandler
{
    private readonly INotificationService _notificationService;
    private readonly IDeviceRepository _deviceRepository;
    private readonly ILogger<FCMNotificationHandler> _logger;

    public FCMNotificationHandler(INotificationService notificationService,
        IDeviceRepository deviceRepository,
        ILogger<FCMNotificationHandler> logger)
    {
        _notificationService = notificationService;
        _deviceRepository = deviceRepository;
        _logger = logger;
    }

    public async Task HandleAsync(SendNotificationEvent @event)
    {
        _logger.LogInformation("Processing FCM notification : {NotificationId} for device: {DeviceId}", @event.NotificationId, @event.DeviceId);

        try
        {
            var notificationDto = new NotificationDto
            {
                Title = @event.Title,
                Body = @event.Body,
                Icon = @event.Icon,
                Data = @event.Data
            };

            var deviceDto = new DeviceDto
            {
                DeviceToken = @event.DeviceToken,
                DeviceType = @event.DeviceType,
                NotificationProvider = @event.NotificationProvider
            };

            var result = await _notificationService.SendNotificationAsync(notificationDto, deviceDto);

            if (result)
            {
                _logger.LogInformation("Notification {NotificationId} sent successfully to device {DeviceId}.", @event.NotificationId, @event.DeviceId);
            }
            else
            {
                _logger.LogWarning("Failed to send notification {NotificationId} to device {DeviceId}.", @event.NotificationId, @event.DeviceId);

                var device = await _deviceRepository.GetByTokenAsync(@event.DeviceToken);
                if (device != null)
                {
                    device.ErrorCount += 1;
                    await _deviceRepository.UpdateAsync(device);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing FCM notification: {NotificationId} for device: {DeviceId}", @event.NotificationId, @event.DeviceId);
        }
    }
}
