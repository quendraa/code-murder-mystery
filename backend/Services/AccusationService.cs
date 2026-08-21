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
            .Include(c => c.Suspects)
            .FirstOrDefaultAsync(c => c.Id == caseId);

        if (caseEntity == null || caseEntity.KillerSuspect == null)
        {
            return null;
        }

        var progress = await context.PlayerProgress
            .FirstOrDefaultAsync(p => p.CaseId == caseId && p.PlayerSessionId == sessionId);

        // Already accused - return the original outcome instead of re-grading
        if (progress != null && progress.CompletedAt != null && progress.AccusedCorrectly.HasValue)
        {
            var originalAccusedName = caseEntity.Suspects
                .FirstOrDefault(s => s.Id == progress.AccusedSuspectId)?.Name;

            return new AccusationResponse(
                IsCorrect: progress.AccusedCorrectly.Value,
                ActualKillerName: caseEntity.KillerSuspect.Name,
                SolutionText: caseEntity.SolutionText,
                AccusedSuspectName: originalAccusedName
            );
        }

        var isCorrect = suspectId == caseEntity.KillerSuspectId;
        var accusedSuspectName = caseEntity.Suspects.FirstOrDefault(s => s.Id == suspectId)?.Name;

        if (progress != null)
        {
            progress.CompletedAt = DateTime.UtcNow;
            progress.AccusedCorrectly = isCorrect;
            progress.AccusedSuspectId = suspectId;
            await context.SaveChangesAsync();
        }

        return new AccusationResponse(
            IsCorrect: isCorrect,
            ActualKillerName: caseEntity.KillerSuspect.Name,
            SolutionText: caseEntity.SolutionText,
            AccusedSuspectName: accusedSuspectName
        );
    }
}
