using MongoDB.Driver;
using ReaLTime.Domain.Entities;
using ReaLTime.Domain.Interfaces.Repositories;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoDeviceRepository : IDeviceRepository
{
    private readonly IMongoCollection<Device> _devices;

    public MongoDeviceRepository(IMongoDatabase database)
    {
        _devices = database.GetCollection<Device>("devices");
    }

    public async Task<Device> GetByIdAsync(string id)
    {
        return await _devices.Find(d => d.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Device> GetByTokenAsync(string token)
    {
        return await _devices.Find(d => d.DeviceToken == token).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(Device device)
    {
        await _devices.InsertOneAsync(device);
    }

    public async Task UpdateAsync(Device device)
    {
        await _devices.ReplaceOneAsync(d => d.Id == device.Id, device);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var result = _devices.DeleteOneAsync(d => d.Id == id);
        return result.Result.DeletedCount > 0;
    }
}