using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using CaseFile.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CasesController(ICaseService caseService) : ControllerBase
{
    private readonly ICaseService _caseService = caseService;

    // GET /api/cases/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<CaseResponse>> GetCase(Guid id)
    {
        var result = await _caseService.GetCaseByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
