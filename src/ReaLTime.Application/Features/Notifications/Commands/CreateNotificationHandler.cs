using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;
using ReaLTime.Shared.Events;
using ReaLTime.Infrastructure.Messaging;

namespace ReaLTime.Application.Features.Notifications.Commands;

public class CreateNotificationCommand
{
    public string CreatorId { get; set; }
    public string Title { get; set; }
    public string? Body { get; set; }
    public string? Icon { get; set; }
    public string? Link { get; set; }
    public Dictionary<string, string>? Data { get; set; }
}

public class CreateNotificationHandler
{
    private readonly INotificationRepository _notificationRepository;
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMessageBus _messageBus;

    public CreateNotificationHandler(INotificationRepository notificationRepository,
        ISubscriptionRepository subscriptionRepository,
        IDeviceRepository deviceRepository, IMessageBus messageBus)
    {
        _notificationRepository = notificationRepository;
        _subscriptionRepository = subscriptionRepository;
        _deviceRepository = deviceRepository;
        _messageBus = messageBus;
    }

    public async Task<string> HandleAsync(CreateNotificationCommand command)
    {
        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            CreatorId = command.CreatorId,
            Title = command.Title,
            Body = command.Body,
            Icon = command.Icon,
            Link = command.Link,
            Data = command.Data,
            CreatedAt = DateTime.UtcNow,
            Status = NotificationStatus.Created
        };
        
        await _notificationRepository.CreateAsync(notification);

        await _messageBus.PublishAsync(new NotificationCreatedEvent
        {
            NotificationId = notification.Id,
            CreatorId = notification.CreatorId,
            Title = notification.Title,
            Body = notification.Body,
            Icon = notification.Icon,
            Link = notification.Link,
            Data = notification.Data,
        });
        
        return notification.Id; 
    }
}