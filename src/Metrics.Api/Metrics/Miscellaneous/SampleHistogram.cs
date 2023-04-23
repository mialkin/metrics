namespace Metrics.Api.Metrics.Miscellaneous;

public static class SampleHistogram
{
    public const string InstrumentName = "file_size_bytes";

    public static readonly double[] Boundaries =
    {
        0.0,
        10000, // 10KB
        20000,
        50000,
        100000, // 100KB
        200000,
        500000,
        1000000, // 1MB
        2000000,
        5000000,
        10000000, // 10MB
        20000000,
        50000000,
        100000000 // 100MB
    };
}