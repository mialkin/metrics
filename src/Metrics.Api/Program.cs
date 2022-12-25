using Metrics.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console()
    .ReadFrom.Configuration(hostBuilderContext.Configuration));

var services = builder.Services;
services.AddControllers();
services.AddHostedService<SampleBackgroundService>();

var app = builder.Build();

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();