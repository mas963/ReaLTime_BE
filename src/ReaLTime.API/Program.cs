using MongoDB.Driver;
using ReaLTime.Domain.Interfaces.Repositories;
using ReaLTime.Infrastructure.Persistence.Repositories;
using Scalar.AspNetCore;
using Wolverine;
using Wolverine.RabbitMQ;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("MongoDB");
    return new MongoClient(connectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoDb:Database"];
    return client.GetDatabase(databaseName);
});

builder.Services.AddScoped<IDeviceRepository, MongoDeviceRepository>();
builder.Services.AddScoped<ISubscriptionRepository, MongoSubscriptionRepository>();
builder.Services.AddScoped<INotificationRepository, MongoNotificationRepository>();

builder.Host.UseWolverine(opts =>
{
    var rabbitmqEndpoint = builder.Configuration.GetConnectionString("RabbitMQ");
    opts.UseRabbitMq(rabbitmqEndpoint)
        .AutoProvision();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapOpenApi();
app.MapScalarApiReference();
app.UseHttpsRedirection();

app.Run();