using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class PlayerProgress
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string PlayerSessionId { get; set; } = string.Empty;

    public Guid CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public Case? Case { get; set; }

    public int CurrentClueIndex { get; set; } = 0;

    // Stored as a comma-separated list of clue GUIDs for simplicity;
    // could move to a proper join table later if querying gets complex
    public string SolvedClueIds { get; set; } = string.Empty;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    public DateTime? CompletedAt { get; set; }

    public bool? AccusedCorrectly { get; set; }

    public Guid? AccusedSuspectId { get; set; }
}