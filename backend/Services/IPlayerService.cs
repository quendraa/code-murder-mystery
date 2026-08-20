using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IPlayerService
{
    Task<PlayerResponse> GetOrCreatePlayerAsync(string sessionId, string detectiveName);
    Task<PlayerResponse?> GetPlayerAsync(string sessionId);
}
