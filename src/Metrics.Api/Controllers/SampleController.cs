using Metrics.Api.Metrics.Meters.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers;

[ApiController]
[Route("sample")]
public class SampleController : ControllerBase
{
    private readonly ISampleControllerMeter _sampleControllerMeter;

    public SampleController(ISampleControllerMeter sampleControllerMeter) =>
        _sampleControllerMeter = sampleControllerMeter;

    [HttpGet("update-processing-time-seconds-gauge")]
    public IActionResult Gauge(int seconds)
    {
        _sampleControllerMeter.UpdateProcessingTimeGauge(seconds);
        
        _sampleControllerMeter.IncrementRequestCounter();

        return Ok();
    }
}