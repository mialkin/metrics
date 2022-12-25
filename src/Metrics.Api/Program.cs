using System;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using Serilog;

var resource = ResourceBuilder.CreateDefault().AddService(serviceName: "Sample metrics service");
var meterName = "Aleksei's meter";
var meter = new Meter(name: meterName, version: "1.0"); // Think of a meter as of a container for all of your metrics
var counter = meter.CreateCounter<long>(name: "Requests");
var histogram = meter.CreateHistogram<long>(name: "RequestDuration");

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(hostBuilderContext.Configuration));

var services = builder.Services;
services.AddControllers();
// services.AddHostedService<SampleBackgroundService>();

services.AddOpenTelemetry().WithMetrics(x =>
{
    x.SetResourceBuilder(resource);
    x.AddConsoleExporter();
    x.AddMeter(meterName);
}).StartWithHost();

var app = builder.Build();

app.MapGet("/", () =>
{
    counter.Add(1);
    histogram.Record(Random.Shared.Next(0, 100));
});

app.MapControllers();

app.Run(); // Run app without debugger to see output in console