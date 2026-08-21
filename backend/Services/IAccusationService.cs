using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IAccusationService
{
    Task<AccusationResponse?> SubmitAccusationAsync(Guid caseId, string sessionId, Guid suspectId);
}
