using System;
using MongoDB.Driver;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoCreatorRepository : ICreatorRepository
{
    private readonly MongoDbContext _context;

    public MongoCreatorRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<Creator> GetByIdAsync(string id)
    {
        return await _context.Creators.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Creator> GetByProfileUrlAsync(string profileUrl)
    {
        return await _context.Creators.Find(c => c.ProfileUrl == profileUrl).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Creator>> GetAllAsync()
    {
        return await _context.Creators.Find(_ => true).ToListAsync();
    }

    public async Task<Creator> CreateAsync(Creator creator)
    {
        creator.CreatedAt = DateTime.UtcNow;
        creator.UpdatedAt = DateTime.UtcNow;
        await _context.Creators.InsertOneAsync(creator);
        return creator;
    }

    public async Task UpdateAsync(Creator creator)
    {
        creator.UpdatedAt = DateTime.UtcNow;
        await _context.Creators.ReplaceOneAsync(c => c.Id == creator.Id, creator);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = await _context.Creators.DeleteOneAsync(c => c.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task IncrementSubscriberCountAsync(string creatorId)
    {
        var update = Builders<Creator>.Update.Inc(c => c.SubscriberCount, 1);
        await _context.Creators.UpdateOneAsync(c => c.Id == creatorId, update);
    }

    public async Task DecrementSubscriberCountAsync(string creatorId)
    {
        var update = Builders<Creator>.Update.Inc(c => c.SubscriberCount, -1);
        await _context.Creators.UpdateOneAsync(c => c.Id == creatorId, update);
    }
}

