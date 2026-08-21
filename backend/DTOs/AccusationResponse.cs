namespace CaseFile.Api.DTOs;

public record class AccusationResponse(
    bool IsCorrect,
    string ActualKillerName,
    string SolutionText
);
