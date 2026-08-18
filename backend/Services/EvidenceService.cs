using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class EvidenceService(CaseFileDbContext context, IPlayerProgressService progressService) : IEvidenceService
{
    public async Task<List<EvidenceResponse>> GetUnlockedEvidenceAsync(Guid caseId, string sessionId)
    {
        var progress = await progressService.GetProgressAsync(caseId, sessionId);

        if (progress == null)
            return [];

        var unlockingClues = await context.Clues
            .Where(c => c.CaseId == caseId && c.EvidenceId != null)
            .Include(c => c.Evidence)
                .ThenInclude(e => e!.LinkedSuspect)
            .ToListAsync();

        var results = new List<EvidenceResponse>();

        foreach (var clue in unlockingClues)
        {
            var isUnlocked = progress.SolvedClueIds.Contains(clue.Id);

            if (!isUnlocked)
                continue;

            var evidence = clue.Evidence!;

            var isReinterpreted = evidence.ReinterpretedAfterClueId.HasValue
                && progress.SolvedClueIds.Contains(evidence.ReinterpretedAfterClueId.Value);

            var description = isReinterpreted
                ? evidence.ReinterpretedDescription ?? evidence.DescriptionText
                : evidence.DescriptionText;

            results.Add(new EvidenceResponse(
                evidence.Id,
                evidence.Title,
                description,
                evidence.LinkedSuspect?.Name
            ));
        }

        return results;
    }
}
