using System;
using MongoDB.Driver;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoNotificationRepository : INotificationRepository
{
    private readonly MongoDbContext _context;

    public MongoNotificationRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Notification> GetByIdAsync(string id)
    {
        return await _context.Notifications.Find(n => n.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Notification>> GetByCreatorIdAsync(string creatorId, int skip, int limit)
    {
        return await _context.Notifications
            .Find(n => n.CreatorId == creatorId)
            .SortByDescending(n => n.CreatedAt)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync();
    }

    public async Task CreateAsync(Notification notification)
    {
        notification.CreatedAt = DateTime.UtcNow;
        await _context.Notifications.InsertOneAsync(notification);
    }

    public async Task UpdateAsync(Notification notification)
    {
        await _context.Notifications.ReplaceOneAsync(n => n.Id == notification.Id, notification);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Notifications.DeleteOneAsync(n => n.Id == id);
        return result.DeletedCount > 0;
    }
}
