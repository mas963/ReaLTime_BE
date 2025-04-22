using Microsoft.Extensions.DependencyInjection;

namespace ReaLTime.Infrastructure.Messaging;

public class RabbitMQMessageBus : IMessageBus
{
    private readonly IServiceProvider _serviceProvider;
    
    public RabbitMQMessageBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task PublishAsync<T>(T message)
    {
        var bus = _serviceProvider.GetRequiredService<IMessageBus>();
        await bus.PublishAsync(message);
    }

    public Task SubscribeAsync<T>(Func<T, Task> handler)
    {
        return Task.CompletedTask;
    }
}
