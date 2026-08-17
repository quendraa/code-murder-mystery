using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class CaseService(CaseFileDbContext context) : ICaseService
{
    private readonly CaseFileDbContext _context = context;

    public async Task<CaseResponse?> GetCaseByIdAsync(Guid id)
    {
        var caseEntity = await _context.Cases
            .Include(c => c.Suspects)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caseEntity == null)
            return null;

        return new CaseResponse(
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
    }
}
