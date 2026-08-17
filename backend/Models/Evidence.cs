using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class Evidence
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public Case? Case { get; set; }

    [Required]
    [MaxLength(150)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string DescriptionText { get; set; } = string.Empty;

    public Guid? LinkedSuspectId { get; set; }

    [ForeignKey(nameof(LinkedSuspectId))]
    public Suspect? LinkedSuspect { get; set; }

    public string? ReinterpretedDescription { get; set; }

    public Guid? ReinterpretedAfterClueId { get; set; }

    [ForeignKey(nameof(ReinterpretedAfterClueId))]
    public Clue? ReinterpretedAfterClue { get; set; }
}
