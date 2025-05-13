using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using ReaLTime.Domain.Interfaces.Repositories;
using ReaLTime.Domain.Interfaces.Services;
using ReaLTime.Infrastructure.Persistence.Repositories;
using ReaLTime.Worker.OneSignal.Services;
using Wolverine;
using Wolverine.RabbitMQ;

namespace ReaLTime.Worker.OneSignal;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;

                services.AddHttpClient();

                services.AddSingleton<INotificationService>(sp =>
                {
                    var httpClient = sp.GetRequiredService<HttpClient>();
                    var appId = configuration["OneSignal:AppId"];
                    var apiKey = configuration["OneSignal:ApiKey"];
                    return new OneSignalNotificationService(httpClient, appId, apiKey);
                });

                services.AddSingleton<IMongoClient>(sp =>
                {
                    var connectionString = configuration.GetConnectionString("MongoDb");
                    return new MongoClient(connectionString);
                });

                services.AddScoped<IMongoDatabase>(sp =>
                {
                    var client = sp.GetRequiredService<IMongoClient>();
                    var databaseName = configuration["MongoDb:DatabaseName"];
                    return client.GetDatabase(databaseName);
                });

                services.AddScoped<IDeviceRepository, MongoDeviceRepository>();
                services.AddScoped<INotificationRepository, MongoNotificationRepository>();

                services.AddHostedService<OneSignalWorkerService>();
            })
            .UseWolverine(opts =>
            {
                opts.UseRabbitMq("amqp://guest:guest@localhost:5672")
                    .AutoProvision();
            });


        builder.Build().Run();
    }
}
