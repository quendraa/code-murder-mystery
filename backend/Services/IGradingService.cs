using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface IGradingService
{
    Task<SubmitSolutionResponse?> GradeSubmissionAsync(Guid clueId, string submittedCode);
}
