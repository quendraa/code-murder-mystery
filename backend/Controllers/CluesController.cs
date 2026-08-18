using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CluesController(IClueService clueService, IGradingService gradingService) : ControllerBase
{
    // GET /api/clues/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<ClueDetailResponse>> GetClue(Guid id)
    {
        var result = await clueService.GetClueByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // POST /api/clues/{id}/submit
    [HttpPost("{id}/submit")]
    public async Task<ActionResult<SubmitSolutionResponse>> SubmitSolution(Guid id, [FromBody] SubmitSolutionRequest request)
    {
        var result = await gradingService.GradeSubmissionAsync(id, request.Code, request.SessionId);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
