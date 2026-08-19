namespace CaseFile.Api.DTOs;

public record class PlayerProgressResponse(
    Guid CaseId,
    string SessionId,
    string? DetectiveName,
    int CurrentClueIndex,
    List<Guid> SolvedClueIds,
    DateTime StartedAt,
    DateTime? CompletedAt
);
