using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProgressController(IPlayerProgressService progressService) : ControllerBase
{
    // POST /api/progress/{caseId}/start
    [HttpPost("{caseId}/start")]
    public async Task<ActionResult<PlayerProgressResponse>> StartProgress(Guid caseId, [FromBody] StartProgressRequest request)
    {
        var result = await progressService.GetOrCreateProgressAsync(caseId, request.SessionId);
        return Ok(result);
    }

    // GET /api/progress/{caseId}?sessionId=xxx
    [HttpGet("{caseId}")]
    public async Task<ActionResult<PlayerProgressResponse>> GetProgress(Guid caseId, [FromQuery] string sessionId)
    {
        var result = await progressService.GetProgressAsync(caseId, sessionId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // DELETE /api/progress/{caseId}?sessionId=xxx
    [HttpDelete("{caseId}")]
    public async Task<IActionResult> ResetProgress(Guid caseId, [FromQuery] string sessionId)
    {
        await progressService.ResetProgressAsync(caseId, sessionId);
        return NoContent();
    }
}
