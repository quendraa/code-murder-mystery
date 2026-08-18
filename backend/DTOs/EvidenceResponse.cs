namespace CaseFile.Api.DTOs;

public record class EvidenceResponse(
    Guid Id,
    string Title,
    string DescriptionText,
    string? LinkedSuspectName
);
