using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CluesController(IClueService clueService) : ControllerBase
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
}
