using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ReaLTime.Worker.FCM;

public class FCMWorkerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<FCMWorkerService> _logger;

    public FCMWorkerService(IServiceProvider serviceProvider, ILogger<FCMWorkerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("FCM Worker service started at: {time}", DateTimeOffset.Now);

        while (!cancellationToken.IsCancellationRequested)
        {
            // Your logic to process notifications goes here
            await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken); // Adjust the delay as needed
        }

        _logger.LogInformation("FCM Worker service stopped at: {time}", DateTimeOffset.Now);
    }
}
