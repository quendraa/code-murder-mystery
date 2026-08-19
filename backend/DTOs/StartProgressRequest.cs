namespace CaseFile.Api.DTOs;

public record class StartProgressRequest(
    string SessionId,
    string? DetectiveName
);
