using System;
using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Interfaces.Repositories;

public interface ISubscriptionRepository
{
    Task<Subscription> GetByIdAsync(string id);
    Task<IEnumerable<Subscription>> GetByCreatorIdAsync(string publisherId);
    Task<IEnumerable<Subscription>> GetByDeviceIdAsync(string deviceId);
    Task<Subscription> GetByDeviceAndCreatorAsync(string deviceId, string creatorId);
    Task CreateAsync(Subscription subscription);
    Task UpdateAsync(Subscription subscription);
    Task<bool> DeleteAsync(string id);
}
