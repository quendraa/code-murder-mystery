namespace CaseFile.Api.DTOs;

public record CaseResponse(
    Guid Id,
    string Title,
    string IntroText,
    string VictimName,
    List<SuspectResponse> Suspects
);
