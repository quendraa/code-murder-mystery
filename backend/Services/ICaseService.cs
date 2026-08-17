using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public interface ICaseService
{
    Task<CaseResponse?> GetCaseByIdAsync(Guid id);
}
