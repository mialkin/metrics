using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Metrics.Api.BackgroundServices;

public class SampleBackgroundService : BackgroundService
{
    private readonly ILogger<SampleBackgroundService> _logger;

    public SampleBackgroundService(ILogger<SampleBackgroundService> logger) => _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        const int seconds = 15;

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Start processing data");

            var processingSeconds = Random.Shared.Next(2, 6);
            await Task.Delay(TimeSpan.FromSeconds(processingSeconds), cancellationToken: stoppingToken);

            _logger.LogInformation("End processing data. Took {ProcessingSeconds} seconds", processingSeconds);

            _logger.LogInformation("Sleeping {Seconds} seconds", seconds);
            await Task.Delay(TimeSpan.FromSeconds(seconds), stoppingToken);
        }
    }
}