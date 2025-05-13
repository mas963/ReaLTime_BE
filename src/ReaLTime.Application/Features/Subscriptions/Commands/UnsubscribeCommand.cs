using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Application.Features.Subscriptions.Commands;

public class UnsubscribeCommand
{
    public string DeviceId { get; set; }
    public string CreatorId { get; set; }
}

public class UnsubscribeCommandHandler
{
    private readonly ISubscriptionRepository _subscriptionRepository;

    public UnsubscribeCommandHandler(ISubscriptionRepository subscriptionRepository)
    {
        _subscriptionRepository = subscriptionRepository;
    }

    public async Task<bool> Handle(UnsubscribeCommand command)
    {
        var subscription = await _subscriptionRepository.GetByDeviceAndCreatorAsync(
            command.DeviceId, command.CreatorId);

        if (subscription == null)
            return false;

        subscription.IsActive = false;
        await _subscriptionRepository.UpdateAsync(subscription);
        return true;
    }
} 