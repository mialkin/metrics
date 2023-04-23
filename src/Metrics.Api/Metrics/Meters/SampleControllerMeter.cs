using System.Diagnostics.Metrics;
using Metrics.Api.Metrics.Infrastructure;
using Metrics.Api.Metrics.Meters.Interfaces;

namespace Metrics.Api.Metrics.Meters;

public class SampleControllerMeter : ISampleControllerMeter
{
    private readonly Counter<long> _requestsCounter;
    private int _processingTimeSeconds;

    public SampleControllerMeter(IDefaultMeterProvider defaultMeterProvider)
    {
        var meter = defaultMeterProvider.Meter;

        _requestsCounter = meter.CreateCounter<long>("sample_controller_requests_total");

        meter.CreateObservableGauge(
            name: "sample_controller_processing_time_seconds",
            observeValue: () => _processingTimeSeconds);
    }

    public void IncrementRequestCounter() => _requestsCounter.Add(1);
    public void UpdateProcessingTimeGauge(int seconds) => _processingTimeSeconds = seconds;
}