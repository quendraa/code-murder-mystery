using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CasesController : ControllerBase
{
    private readonly CaseFileDbContext _context;

    public CasesController(CaseFileDbContext context)
    {
        _context = context;
    }

    // GET /api/cases/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CaseResponse>> GetCase(Guid id)
    {
        var caseEntity = await _context.Cases
            .Include(c => c.Suspects)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caseEntity == null)
            return NotFound();

        var response = new CaseResponse(
            caseEntity.Id,
            caseEntity.Title,
            caseEntity.IntroText,
            caseEntity.VictimName,
            [.. caseEntity.Suspects.Select(s => new SuspectResponse(
                s.Id,
                s.Name,
                s.Bio,
                s.AlibiText
            ))]
        );

        return Ok(response);
    }
}
