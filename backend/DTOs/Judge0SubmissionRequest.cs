using System.Text.Json.Serialization;

namespace CaseFile.Api.DTOs;

public record Judge0SubmissionRequest(
    [property: JsonPropertyName("source_code")] string source_code,
    [property: JsonPropertyName("language_id")] int language_id,
    [property: JsonPropertyName("stdin")] string stdin
);