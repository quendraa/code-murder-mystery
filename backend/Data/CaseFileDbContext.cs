using CaseFile.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Data;

public class CaseFileDbContext : DbContext
{
    public CaseFileDbContext(DbContextOptions<CaseFileDbContext> options) : base(options)
    {
    }

    public DbSet<Case> Cases { get; set; }
    public DbSet<Suspect> Suspects { get; set; }
    public DbSet<Clue> Clues { get; set; }
    public DbSet<PuzzleTestCase> PuzzleTestCases { get; set; }
    public DbSet<Evidence> Evidence { get; set; }
    public DbSet<PlayerProgress> PlayerProgress { get; set; }
    public DbSet<Player> Player { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Case → KillerSuspect: prevent cascade delete conflicts
        // (a case shouldn't be deletable in a way that also silently deletes a suspect, or vice versa causing a cycle)
        modelBuilder.Entity<Case>()
            .HasOne(c => c.KillerSuspect)
            .WithMany()
            .HasForeignKey(c => c.KillerSuspectId)
            .OnDelete(DeleteBehavior.Restrict);

        // Evidence → ReinterpretedAfterClue: also restrict, avoids a delete cycle
        // between Clue -> Evidence (unlocks) and Evidence -> Clue (reinterprets)
        modelBuilder.Entity<Evidence>()
            .HasOne(e => e.ReinterpretedAfterClue)
            .WithMany()
            .HasForeignKey(e => e.ReinterpretedAfterClueId)
            .OnDelete(DeleteBehavior.Restrict);

        // Clue → Evidence (unlocks): also restrict for the same cycle-prevention reason
        modelBuilder.Entity<Clue>()
            .HasOne(c => c.Evidence)
            .WithMany()
            .HasForeignKey(c => c.EvidenceId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>()
            .HasIndex(p => p.SessionId)
            .IsUnique();

        modelBuilder.Entity<PlayerProgress>()
            .HasIndex(p => new { p.CaseId, p.PlayerSessionId })
            .IsUnique();
    }
}
