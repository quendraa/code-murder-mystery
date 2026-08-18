using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class Clue
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public Case? Case { get; set; }

    [Required]
    public int OrderIndex { get; set; }

    [Required]
    [MaxLength(150)]
    public string SourceLabel { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string PuzzleType { get; set; } = string.Empty;

    [Required]
    public string PromptText { get; set; } = string.Empty;

    [Required]
    public string StarterCode { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string Language { get; set; } = "javascript";

    public Guid? EvidenceId { get; set; }

    [ForeignKey(nameof(EvidenceId))]
    public Evidence? Evidence { get; set; }

    [Required]
    [MaxLength(100)]
    public string FunctionName { get; set; } = string.Empty;

    public ICollection<PuzzleTestCase> TestCases { get; set; } = new List<PuzzleTestCase>();
}