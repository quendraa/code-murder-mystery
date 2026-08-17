using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaseFile.Api.Models;

public class Suspect
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    public Guid CaseId { get; set; }

    [ForeignKey(nameof(CaseId))]
    public Case? Case { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Bio { get; set; } = string.Empty;

    [Required]
    public string AlibiText { get; set; } = string.Empty;

    public bool IsKiller { get; set; } = false;

    public ICollection<Evidence> LinkedEvidence { get; set; } = new List<Evidence>();
}
