using System.Diagnostics.Metrics;

namespace Metrics.Api.Metrics.Infrastructure;

public interface IDefaultMeterProvider
{
    public Meter Meter { get; }
}