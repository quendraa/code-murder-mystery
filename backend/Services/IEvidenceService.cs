using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IEvidenceService
{
    Task<List<EvidenceResponse>> GetUnlockedEvidenceAsync(Guid caseId, string sessionId);
}
