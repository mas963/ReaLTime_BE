using System;
using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Interfaces.Repositories;

public interface INotificationRepository
{
    Task<Notification> GetByIdAsync(string id);
    Task<IEnumerable<Notification>> GetByCreatorIdAsync(string creatorId, int skip, int limit);
    Task CreateAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task<bool> DeleteAsync(string id);
}
