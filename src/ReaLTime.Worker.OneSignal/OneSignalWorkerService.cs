using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ReaLTime.Worker.OneSignal;

public class OneSignalWorkerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<OneSignalWorkerService> _logger;

    public OneSignalWorkerService(IServiceProvider serviceProvider, ILogger<OneSignalWorkerService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("OneSignal Worker service started at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Adjust the delay as needed
        }

        _logger.LogInformation("OneSignal Worker service stopped at: {time}", DateTimeOffset.Now);
    }
}
