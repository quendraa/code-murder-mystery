using System.ComponentModel.DataAnnotations;

namespace CaseFile.Api.Models;

public class Player
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(100)]
    public string SessionId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string DetectiveName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<PlayerProgress> ProgressRecords { get; set; } = new List<PlayerProgress>();
}
