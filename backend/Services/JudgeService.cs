using System.Text;
using System.Text.Json;
using CaseFile.Api.DTOs;

namespace CaseFile.Api.Services;

public class JudgeService(HttpClient httpClient) : IJudgeService
{
    public async Task<(bool Success, string? Stdout, string? Stderr)> RunCodeAsync(string sourceCode, string stdin)
    {
        var requestBody = new Judge0SubmissionRequest(sourceCode, 63, stdin);
        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(
            "submissions?base64_encoded=false&wait=true",
            content
        );

        var rawResponseBody = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<Judge0SubmissionResponse>(rawResponseBody);

        var isAccepted = result?.status?.id == 3;

        return (isAccepted, result?.stdout, result?.stderr);
    }
}