namespace CaseFile.Api.DTOs;

public record class AccusationRequest(
    string SessionId,
    Guid SuspectId
);
