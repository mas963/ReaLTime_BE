using System;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using ReaLTime.Domain.Entities;

namespace ReaLTime.Infrastructure.Persistence.Repositories;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings) 
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Creator> Creators => _database.GetCollection<Creator>("Creators");
    public IMongoCollection<Subscription> Subscriptions => _database.GetCollection<Subscription>("Subscriptions");
    public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
    public IMongoCollection<Device> Devices => _database.GetCollection<Device>("Devices");
}

public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
