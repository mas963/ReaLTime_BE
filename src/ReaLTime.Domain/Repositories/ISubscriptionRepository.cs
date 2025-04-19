using System;
using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetByIdAsync(string id);
    Task<IEnumerable<Subscription>> GetByCreatorIdAsync(string creatorId);
    Task<IEnumerable<Subscription>> GetByDeviceIdAsync(string deviceId);
    Task<Subscription> GetByCreatorAndDeviceAsync(string creatorId, string deviceId);
    Task<Subscription> CreateAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task<bool> DeleteAsync(string id);
    Task<bool> DeactivateAsync(string id);
    Task<long> GetActiveSubscriptionCountForCreatorAsync(string creatorId);
    Task<IEnumerable<Subscription>> GetActiveSubscriptionsForCreatorAsync(string creatorId, int skip, int limit);
}
