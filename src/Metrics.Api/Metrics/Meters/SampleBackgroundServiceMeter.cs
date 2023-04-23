using Metrics.Api.Metrics.Infrastructure;
using Metrics.Api.Metrics.Meters.Interfaces;

namespace Metrics.Api.Metrics.Meters;

public class SampleBackgroundServiceMeter : ISampleBackgroundServiceMeter
{
    public SampleBackgroundServiceMeter(IDefaultMeterProvider defaultMeterProvider)
    {
        var meter = defaultMeterProvider.Meter;

    }
}