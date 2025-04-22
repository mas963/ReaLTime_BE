using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Interfaces.Repositories;

public interface IDeviceRepository
{
    Task<Device> GetByIdAsync(string id);
    Task<Device> GetByTokenAsync(string token);
    Task CreateAsync(Device device);
    Task UpdateAsync(Device device);
    Task<bool> DeleteAsync(string id);
}