using ReaLTime.Application.Common.Exceptions;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Application.Features.Subscriptions.Commands;

public class SubscribeCommand
{
    public string DeviceId { get; set; }
    public string CreatorId { get; set; }
}

public class SubscribeCommandHandler
{
    private readonly ISubscriptionRepository _subscriptionRepository;
    private readonly IDeviceRepository _deviceRepository;
    
    public SubscribeCommandHandler(ISubscriptionRepository subscriptionRepository, IDeviceRepository deviceRepository)
    {
        _subscriptionRepository = subscriptionRepository;
        _deviceRepository = deviceRepository;
    }

    public async Task<bool> HandleAsync(SubscribeCommand command)
    {
        var device = await _deviceRepository.GetByIdAsync(command.DeviceId);
        if (device == null) 
            throw new NotFoundException("Device not found");
        
        var existingSubscription = await _subscriptionRepository.GetByDeviceAndCreatorAsync(command.DeviceId, command.CreatorId);

        if (existingSubscription != null)
        {
            if (!existingSubscription.IsActive)
            {
                existingSubscription.IsActive = true;
                await _subscriptionRepository.UpdateAsync(existingSubscription);
            }
            return true;
        }
        
        var subscription = new Domain.Entities.Subscription
        {
            Id = Guid.NewGuid().ToString(),
            DeviceId = command.DeviceId,
            CreatorId = command.CreatorId,
            SubscribedAt = DateTime.UtcNow,
            IsActive = true,
        };

        await _subscriptionRepository.CreateAsync(subscription);
        return true;
    }
}