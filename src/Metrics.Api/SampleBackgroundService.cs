using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Metrics.Api;

public class SampleBackgroundService : BackgroundService
{
    private readonly ILogger<SampleBackgroundService> _logger;

    public SampleBackgroundService(ILogger<SampleBackgroundService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Processing data. Current date: {Date}", DateTime.UtcNow);

            await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
        }
    }
}