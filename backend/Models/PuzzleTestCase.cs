using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class PuzzleTestCase
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid ClueId { get; set; }

    [ForeignKey(nameof(ClueId))]
    public Clue? Clue { get; set; }

    [Required]
    public string Input { get; set; } = string.Empty;

    [Required]
    public string ExpectedOutput { get; set; } = string.Empty;

    // Visible test cases show in the UI as examples; hidden ones only run on submit
    public bool IsHidden { get; set; } = false;
}
