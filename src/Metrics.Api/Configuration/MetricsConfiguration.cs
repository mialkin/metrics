using System.Diagnostics.Metrics;
using System.Reflection;
using Metrics.Api.Metrics.Infrastructure;
using Metrics.Api.Metrics.Meters;
using Metrics.Api.Metrics.Meters.Interfaces;
using Metrics.Api.Metrics.Miscellaneous;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace Metrics.Api.Configuration;

public static class MetricsConfiguration
{
    public static void ConfigureMetrics(this IServiceCollection services)
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName: assemblyName!);
        const string defaultMeterName = nameof(DefaultMeterProvider);

        services.AddOpenTelemetry()
            .WithMetrics(x =>
            {
                x.SetResourceBuilder(resourceBuilder);
                x.AddPrometheusExporter();
                x.AddMeter(defaultMeterName);
                x.AddView(
                    instrumentName: ResponseTimeHistogram.InstrumentName,
                    metricStreamConfiguration: new ExplicitBucketHistogramConfiguration
                    {
                        Boundaries = ResponseTimeHistogram.Boundaries
                    });

                // x.AddConsoleExporter(); // Requires OpenTelemetry.Exporter.Console package
                // x.AddRuntimeInstrumentation(); // Requires OpenTelemetry.Instrumentation.Runtime package
            })
            .StartWithHost(); // Requires OpenTelemetry.Extensions.Hosting package

        ConfigureMeters(services, defaultMeterName);
    }

    private static void ConfigureMeters(IServiceCollection services, string defaultMeterName)
    {
        services.AddSingleton<IDefaultMeterProvider>(_ =>
        {
            var meter = new Meter(name: defaultMeterName, version: "1.0.0");
            return new DefaultMeterProvider(meter);
        });

        services.AddSingleton<ISampleControllerMeter, SampleControllerMeter>();
        services.AddSingleton<ISampleBackgroundServiceMeter, SampleBackgroundServiceMeter>();
    }
}