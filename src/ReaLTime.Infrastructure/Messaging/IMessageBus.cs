using System;

namespace ReaLTime.Infrastructure.Messaging;

public interface IMessageBus
{
    Task PublishAsync<T>(T message);
    Task SubscribeAsync<T>(Func<T, Task> handler);
}
