namespace Metrics.Api.Metrics.Configuration;

public static class ResponseTimeHistogram
{
    public const string InstrumentName = "response_time_seconds";

    public static readonly double[] Boundaries =
    {
        0.0,
        0.001,
        0.01,
        0.1,
        1,
        10
    };
}