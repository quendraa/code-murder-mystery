using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class AccusationService(CaseFileDbContext context) : IAccusationService
{
    public async Task<AccusationResponse?> SubmitAccusationAsync(Guid caseId, string sessionId, Guid suspectId)
    {
        var caseEntity = await context.Cases
            .Include(c => c.KillerSuspect)
            .FirstOrDefaultAsync(c => c.Id == caseId);

        if (caseEntity == null || caseEntity.KillerSuspect == null)
            return null;

        var progress = await context.PlayerProgress
            .FirstOrDefaultAsync(p => p.CaseId == caseId && p.PlayerSessionId == sessionId);

        if (progress != null && progress.CompletedAt == null)
        {
            progress.CompletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync();
        }

        var isCorrect = suspectId == caseEntity.KillerSuspectId;

        return new AccusationResponse(
            isCorrect,
            caseEntity.KillerSuspect.Name,
            caseEntity.SolutionText
        );
    }
}
