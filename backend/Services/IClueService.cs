using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IClueService
{
    Task<List<ClueSummaryResponse>> GetCluesByCaseIdAsync(Guid caseId);
    Task<ClueDetailResponse?> GetClueByIdAsync(Guid id);
}
