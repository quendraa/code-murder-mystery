using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EvidenceController(IEvidenceService evidenceService) : ControllerBase
{
    // GET /api/evidence/{caseId}?sessionId=xxx
    [HttpGet("{caseId}")]
    public async Task<ActionResult<List<EvidenceResponse>>> GetEvidence(Guid caseId, [FromQuery] string sessionId)
    {
        var result = await evidenceService.GetUnlockedEvidenceAsync(caseId, sessionId);
        return Ok(result);
    }
}
