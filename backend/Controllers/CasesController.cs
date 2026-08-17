using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CasesController(ICaseService caseService, IClueService clueService) : ControllerBase
{
    // GET /api/cases/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CaseResponse>> GetCase(Guid id)
    {
        var result = await caseService.GetCaseByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // GET /api/cases/{caseId}/clues
    [HttpGet("{caseId}/clues")]
    public async Task<ActionResult<List<ClueSummaryResponse>>> GetCluesByCase(Guid caseId)
    {
        var result = await clueService.GetCluesByCaseIdAsync(caseId);
        return Ok(result);
    }
}
