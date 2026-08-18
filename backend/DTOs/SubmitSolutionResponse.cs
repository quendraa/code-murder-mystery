namespace CaseFile.Api.DTOs;

public record class SubmitSolutionResponse(
    bool AllTestsPassed,
    List<TestCaseResult> Results,
    string? UnlockedEvidenceTitle
);
