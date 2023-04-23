namespace Metrics.Api.Metrics.Meters.Interfaces;

public interface ISampleControllerMeter
{
    void IncrementRequestCounter();
    void UpdateProcessingTimeGauge(int seconds);
}