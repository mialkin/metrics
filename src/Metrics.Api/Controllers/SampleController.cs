using System;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers;

[ApiController]
[Route("sample")]
public class SampleController : ControllerBase
{
    [HttpGet("gauge")]
    public IActionResult Gauge(int instantValue)
    {
        return Ok(DateTime.UtcNow);
    }
}