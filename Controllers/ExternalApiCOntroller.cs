using Microsoft.AspNetCore.Mvc;
using TaskApi.Application.Interfaces;

namespace TaskApi.Controllers;

[ApiController]
[Route("api/external")]
public class ExternalController : ControllerBase
{
    private readonly IExternalApiClient _client;

    public ExternalController(IExternalApiClient client) => _client = client;

    [HttpGet("sample")]
    public async Task<IActionResult> GetSample(CancellationToken ct)
    {
        var result = await _client.GetSampleDataAsync(ct);
        return Ok(result);
    }
}
