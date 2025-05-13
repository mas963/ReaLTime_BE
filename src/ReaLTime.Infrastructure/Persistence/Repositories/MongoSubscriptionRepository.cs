using System;
using MongoDB.Driver;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoSubscriptionRepository : ISubscriptionRepository
{
    private readonly IMongoCollection<Subscription> _subscription;

    public MongoSubscriptionRepository(IMongoDatabase database)
    {
        _subscription = database.GetCollection<Subscription>("subscriptions");
    }

    public async Task<Subscription> GetByIdAsync(string id)
    {
        return await _subscription.Find(s => s.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByCreatorIdAsync(string creatorId)
    {
        return await _subscription
            .Find(s => s.CreatorId == creatorId && s.IsActive == true)
            .ToListAsync();
    }

    public async Task<IEnumerable<Subscription>> GetByDeviceIdAsync(string deviceId)
    {
        return await _subscription.Find(s => s.DeviceId == deviceId).ToListAsync();
    }

    public async Task<Subscription> GetByDeviceAndCreatorAsync(string deviceId, string creatorId)
    {
        return await _subscription
            .Find(s => s.DeviceId == deviceId && s.CreatorId == creatorId)
            .FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Subscription subscription)
    {
        subscription.SubscribedAt = DateTime.UtcNow;
        subscription.IsActive = true;
        await _subscription.InsertOneAsync(subscription);
    }

    public async Task UpdateAsync(Subscription subscription)
    {
        await _subscription.ReplaceOneAsync(s => s.Id == subscription.Id, subscription);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _subscription.DeleteOneAsync(s => s.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<bool> DeactivateAsync(string id)
    {
        var update = Builders<Subscription>.Update
            .Set(s => s.IsActive, false);

        var result = await _subscription.UpdateOneAsync(s => s.Id == id, update);
        return result.ModifiedCount > 0;
    }

    public async Task<long> GetActiveSubscriptionCountForCreatorAsync(string creatorId)
    {
        return await _subscription
            .CountDocumentsAsync(s => s.CreatorId == creatorId && s.IsActive);
    }

    public async Task<IEnumerable<Subscription>> GetActiveSubscriptionsForCreatorAsync(string creatorId, int skip, int limit)
    {
        return await _subscription
            .Find(s => s.CreatorId == creatorId && s.IsActive)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }
}
