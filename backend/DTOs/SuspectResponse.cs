namespace CaseFile.Api.DTOs;

public record class SuspectResponse(
    Guid Id,
    string Name,
    string Bio,
    string AlibiText
);
