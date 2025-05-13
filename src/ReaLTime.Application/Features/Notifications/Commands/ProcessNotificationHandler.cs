using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;
using ReaLTime.Infrastructure.Messaging;
using ReaLTime.Shared.Events;

namespace ReaLTime.Application.Features.Notifications.Commands;

public class ProcessNotificationHandler
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMessageBus _messageBus;

    public ProcessNotificationHandler(INotificationRepository notificationRepository, ISubscriptionRepository subscriptionRepository, IDeviceRepository deviceRepository, IMessageBus messageBus)
    {
        _notificationRepository = notificationRepository;
        _subscriptionRepository = subscriptionRepository;
        _deviceRepository = deviceRepository;
        _messageBus = messageBus;
    }

    public async Task HandleAsync(NotificationCreatedEvent @event)
    {
        var notification = await _notificationRepository.GetByIdAsync(@event.NotificationId);
        if (notification == null)
            return;

        notification.Status = NotificationStatus.Processing;
        await _notificationRepository.UpdateAsync(notification);

        var subscriptions = await _subscriptionRepository.GetByCreatorIdAsync(@event.CreatorId);

        foreach (var subscription in subscriptions)
        {
            var device = await _deviceRepository.GetByIdAsync(subscription.DeviceId);
            if (device == null)
                continue;

            await _messageBus.PublishAsync(new SendNotificationEvent
            {
                NotificationId = notification.Id,
                DeviceId = device.Id,
                DeviceToken = device.DeviceToken,
                DeviceType = device.DeviceType,
                NotificationProvider = device.NotificationProvider,
                Title = notification.Title,
                Body = notification.Body,
                Icon = notification.Icon,
                Data = notification.Data
            });
        }
    }
}
