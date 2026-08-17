using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class ClueService(CaseFileDbContext context) : IClueService
{
    public async Task<ClueDetailResponse?> GetClueByIdAsync(Guid id)
    {
        var clueEntity = await context.Clues
        .Include(c => c.TestCases)
        .FirstOrDefaultAsync(c => c.Id == id);

        if (clueEntity == null)
            return null;

        return new ClueDetailResponse(
            clueEntity.Id,
            clueEntity.OrderIndex,
            clueEntity.SourceLabel,
            clueEntity.PuzzleType,
            clueEntity.PromptText,
            clueEntity.StarterCode,
            clueEntity.Language,
            [.. clueEntity.TestCases
                .Where(t => !t.IsHidden)
                .Select(t => new PuzzleTestCaseResponse(
                    t.Input,
                    t.ExpectedOutput
                ))]
        );
    }

    public async Task<List<ClueSummaryResponse>> GetCluesByCaseIdAsync(Guid caseId)
    {
        var clueEntityList = await context.Clues
            .Where(c => c.CaseId == caseId)
            .OrderBy(c => c.OrderIndex)
            .ToListAsync();

        return [.. clueEntityList.Select(c => new ClueSummaryResponse(
            c.Id,
            c.OrderIndex,
            c.SourceLabel,
            c.PuzzleType
        ))];
    }
}
