using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class Case
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string IntroText { get; set; } = string.Empty;

    [Required]
    [MaxLength(200)]
    public string VictimName { get; set; } = string.Empty;

    [Required]
    public string SolutionText { get; set; } = string.Empty;

    public Guid? KillerSuspectId { get; set; }

    [ForeignKey(nameof(KillerSuspectId))]
    public Suspect? KillerSuspect { get; set; }

    public ICollection<Suspect> Suspects { get; set; } = new List<Suspect>();
    public ICollection<Clue> Clues { get; set; } = new List<Clue>();
    public ICollection<Evidence> Evidence { get; set; } = new List<Evidence>();
}