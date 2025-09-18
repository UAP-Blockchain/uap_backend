using Microsoft.AspNetCore.Mvc;

namespace Fap.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok("OK");
}
