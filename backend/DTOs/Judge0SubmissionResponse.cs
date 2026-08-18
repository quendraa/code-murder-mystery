using System.Text.Json.Serialization;

namespace CaseFile.Api.DTOs;

public record Judge0SubmissionResponse(
    [property: JsonPropertyName("stdout")] string? stdout,
    [property: JsonPropertyName("stderr")] string? stderr,
    [property: JsonPropertyName("message")] string? message,
    [property: JsonPropertyName("status")] Judge0Status? status
);

public record Judge0Status(
    [property: JsonPropertyName("id")] int id,
    [property: JsonPropertyName("description")] string description
);
