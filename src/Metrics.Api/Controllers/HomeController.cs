using System;
using Microsoft.AspNetCore.Mvc;

namespace Metrics.Api.Controllers;

[ApiController]
[Route("home")]
public class HomeController : ControllerBase
{
    [HttpGet("index")]
    public IActionResult Index()
    {
        return Ok(DateTime.UtcNow);
    }
}