using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(hostBuilderContext.Configuration));

var services = builder.Services;
services.AddControllers();
// services.AddHostedService<SampleBackgroundService>();

var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName: "Sample metrics service");
const string meterName = "Aleksei's meter";

services
    .AddOpenTelemetry()
    .WithMetrics(x =>
    {
        x.SetResourceBuilder(resourceBuilder);
        // x.AddConsoleExporter(); // Requires OpenTelemetry.Exporter.Console package
        x.AddPrometheusExporter(); // Requires OpenTelemetry.Exporter.Prometheus.AspNetCore package
        x.AddMeter(meterName);
        // x.AddRuntimeInstrumentation(); // Requires OpenTelemetry.Instrumentation.Runtime package
    })
    .StartWithHost(); // Requires OpenTelemetry.Extensions.Hosting package

var app = builder.Build();

var meter = new Meter(name: meterName, version: "1.0"); // Think of a meter as of a container for all of your metrics
var counter = meter.CreateCounter<long>(name: "Requests");
var histogram = meter.CreateHistogram<long>(name: "RequestDuration");

app.MapGet("/", () =>
{
    counter.Add(1, new KeyValuePair<string, object?>("color", "red"));
    counter.Add(2, new KeyValuePair<string, object?>("color", "orange"));
    histogram.Record(Random.Shared.Next(0, 100));
});

app.MapControllers();
app.UseOpenTelemetryPrometheusScrapingEndpoint(); // Requires OpenTelemetry.Exporter.Prometheus.AspNetCore package
// https://github.com/open-telemetry/opentelemetry-dotnet/blob/main/src/OpenTelemetry.Exporter.Prometheus.AspNetCore/README.md

app.Run(); // Run app without debugger to see output in console