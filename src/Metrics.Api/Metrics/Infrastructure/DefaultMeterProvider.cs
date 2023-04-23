using System.Diagnostics.Metrics;

namespace Metrics.Api.Metrics.Infrastructure;

public class DefaultMeterProvider : IDefaultMeterProvider
{
    public Meter Meter { get; }

    public DefaultMeterProvider(Meter meter) => Meter = meter;
}