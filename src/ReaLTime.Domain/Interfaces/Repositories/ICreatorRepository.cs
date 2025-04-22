using System;
using ReaLTime.Domain.Entities;

namespace ReaLTime.Domain.Interfaces.Repositories;

public interface ICreatorRepository
{
    Task<Creator> GetByIdAsync(string id);
    Task<Creator> GetByProfileUrlAsync(string profileUrl);
    Task<IEnumerable<Creator>> GetAllAsync();
    Task<Creator> CreateAsync(Creator creator);
    Task UpdateAsync(Creator creator);
    Task<bool> DeleteAsync(string id);
    Task IncrementSubscriberCountAsync(string creatorId);
    Task DecrementSubscriberCountAsync(string creatorId);
}
