namespace CaseFile.Api.DTOs;

public record class TestCaseResult(
    bool Passed,
    bool IsHidden,
    string? Input,
    string? ExpectedOutput,
    string? ActualOutput,
    string? ErrorMessage
);
