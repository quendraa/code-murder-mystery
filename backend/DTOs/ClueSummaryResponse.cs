namespace CaseFile.Api.DTOs;

public record ClueSummaryResponse(
    Guid Id,
    int OrderIndex,
    string SourceLabel,
    string PuzzleType
);
