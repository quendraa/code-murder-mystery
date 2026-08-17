namespace CaseFile.Api.DTOs;

public record ClueDetailResponse(
    Guid Id,
    int OrderIndex,
    string SourceLabel,
    string PuzzleType,
    string PromptText,
    string StarterCode,
    string Language,
    List<PuzzleTestCaseResponse> VisibleTestCases
);
