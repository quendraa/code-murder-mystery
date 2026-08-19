namespace CaseFile.Api.DTOs;

public record class CaseSummaryResponse(
    Guid Id,
    string Title,
    string VictimName
);