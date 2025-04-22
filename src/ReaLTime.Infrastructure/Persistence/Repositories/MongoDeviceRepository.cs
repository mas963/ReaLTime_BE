using MongoDB.Driver;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoDeviceRepository : IDeviceRepository
{
    private readonly MongoDbContext _context;
    
    public MongoDeviceRepository(MongoDbContext context)
    {
        _context = context;
    }
    
    public async Task<Device> GetByIdAsync(string id)
    {
        return await _context.Devices.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Device> GetByTokenAsync(string token)
    {
        return await _context.Devices.Find(d => d.DeviceToken == token).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Device device)
    {
        await _context.Devices.InsertOneAsync(device);
    }

    public async Task UpdateAsync(Device device)
    {
        await _context.Devices.ReplaceOneAsync(d => d.Id == device.Id, device);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = _context.Devices.DeleteOneAsync(d => d.Id == id);
        return result.Result.DeletedCount > 0;
    }
}