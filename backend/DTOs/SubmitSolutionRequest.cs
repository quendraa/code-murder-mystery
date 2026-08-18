namespace CaseFile.Api.DTOs;

public record class SubmitSolutionRequest(
    string Code,
    string SessionId
);
