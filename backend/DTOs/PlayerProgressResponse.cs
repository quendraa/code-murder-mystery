namespace CaseFile.Api.DTOs;

public record class PlayerProgressResponse(
    Guid CaseId,
    string SessionId,
    int CurrentClueIndex,
    List<Guid> SolvedClueIds,
    DateTime StartedAt,
    DateTime? CompletedAt
);
