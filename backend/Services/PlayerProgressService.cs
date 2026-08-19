using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using CaseFile.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class PlayerProgressService(CaseFileDbContext context) : IPlayerProgressService
{
    public async Task<PlayerProgressResponse> GetOrCreateProgressAsync(Guid caseId, string sessionId, string? detectiveName = null)
    {
        var progress = await context.PlayerProgress
            .FirstOrDefaultAsync(p => p.CaseId == caseId && p.PlayerSessionId == sessionId);

        if (progress == null)
        {
            progress = new PlayerProgress
            {
                Id = Guid.NewGuid(),
                CaseId = caseId,
                PlayerSessionId = sessionId,
                DetectiveName = detectiveName,
                CurrentClueIndex = 0,
                SolvedClueIds = string.Empty,
                StartedAt = DateTime.UtcNow
            };

            context.PlayerProgress.Add(progress);
            await context.SaveChangesAsync();
        }

        return MapToResponse(progress);
    }

    public async Task<PlayerProgressResponse?> GetProgressAsync(Guid caseId, string sessionId)
    {
        var progress = await context.PlayerProgress
            .FirstOrDefaultAsync(p => p.CaseId == caseId && p.PlayerSessionId == sessionId);

        return progress == null ? null : MapToResponse(progress);
    }

    public async Task MarkClueSolvedAsync(Guid caseId, string sessionId, Guid clueId)
    {
        var progress = await context.PlayerProgress
            .FirstOrDefaultAsync(p => p.CaseId == caseId && p.PlayerSessionId == sessionId);

        if (progress == null)
            return;

        var solvedIds = ParseSolvedIds(progress.SolvedClueIds);

        if (!solvedIds.Contains(clueId))
        {
            solvedIds.Add(clueId);
            progress.SolvedClueIds = string.Join(",", solvedIds);
            progress.CurrentClueIndex += 1;
            await context.SaveChangesAsync();
        }
    }

    private static List<Guid> ParseSolvedIds(string raw) =>
        string.IsNullOrWhiteSpace(raw)
            ? []
            : [.. raw.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse)];

    private static PlayerProgressResponse MapToResponse(PlayerProgress progress) =>
        new(
            progress.CaseId,
            progress.PlayerSessionId,
            progress.DetectiveName,
            progress.CurrentClueIndex,
            ParseSolvedIds(progress.SolvedClueIds),
            progress.StartedAt,
            progress.CompletedAt
        );
}
