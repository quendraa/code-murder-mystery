namespace CaseFile.Api.DTOs;

public record class PlayerResponse(
    Guid Id,
    string SessionId,
    string DetectiveName,
    DateTime CreatedAt
);
