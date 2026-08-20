using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayerController(IPlayerService playerService) : ControllerBase
{
    // POST /api/player/register
    [HttpPost("register")]
    public async Task<ActionResult<PlayerResponse>> Register([FromBody] RegisterPlayerRequest request)
    {
        var result = await playerService.GetOrCreatePlayerAsync(request.SessionId, request.DetectiveName);
        return Ok(result);
    }

    // GET /api/player
    [HttpGet]
    public async Task<ActionResult<PlayerResponse>> GetPlayer([FromQuery] string sessionId)
    {
        var result = await playerService.GetPlayerAsync(sessionId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
