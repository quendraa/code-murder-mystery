using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IPlayerProgressService
{
    Task<PlayerProgressResponse> GetOrCreateProgressAsync(Guid caseId, string sessionId);
    Task<PlayerProgressResponse?> GetProgressAsync(Guid caseId, string sessionId);
    Task MarkClueSolvedAsync(Guid caseId, string sessionId, Guid clueId);
}
