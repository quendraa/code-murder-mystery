namespace CaseFile.Api.DTOs;

public record SuspectResponse(
    Guid Id,
    string Name,
    string Bio,
    string AlibiText
);
