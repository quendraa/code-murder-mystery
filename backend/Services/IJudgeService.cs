namespace CaseFile.Api.Services;

public interface IJudgeService
{
    // Tuple for now is fine, maybe latter something else when it grows.
    Task<(bool Success, string? Stdout, string? Stderr)> RunCodeAsync(string sourceCode, string stdin);
}
