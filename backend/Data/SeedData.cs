using CaseFile.Api.Models;

namespace CaseFile.Api.Data;

public static class SeedData
{
    public static void Seed(CaseFileDbContext context)
    {
        if (context.Cases.Any())
            return; // It is already seeded
        
        var caseId = Guid.NewGuid();

        var priya = new Suspect
        {
            Id = Guid.NewGuid(),
            CaseId = caseId,
            Name = "Priya Patel",
            Bio = "Co-founder and CEO of Nexlify. Driven, protective of the company's momentum.",
            AlibiText = "Says she left the office at 11:30 PM and was home by midnight.",
            IsKiller = true 
        };

        var dev = new Suspect
        {
            Id = Guid.NewGuid(),
            CaseId = caseId,
            Name = "Dev Okafor",
            Bio = "Junior engineer. Marcus was about to fire him over a data leak bug.",
            AlibiText = "Claims he pushed a code change remotely and was never in the building.",
            IsKiller = false
        };

        var sarah = new Suspect
        {
            Id = Guid.NewGuid(),
            CaseId = caseId,
            Name = "Sarah Kim",
            Bio = "Company counsel, Marcus's ex-girlfriend. Their breakup was recent and messy.",
            AlibiText = "Says she was working late in her own office, alone.",
            IsKiller = false
        };

        var tom = new Suspect
        {
            Id = Guid.NewGuid(),
            CaseId = caseId,
            Name = "Tom Reyes",
            Bio = "Lead investor. Stood to lose millions if the funding round collapsed.",
            AlibiText = "Claims he was at a dinner across town until 1 AM.",
            IsKiller = false
        };

        var alex = new Suspect
        {
            Id = Guid.NewGuid(),
            CaseId = caseId,
            Name = "Alex Park",
            Bio = "Intern. Seems unconnected to the case at first glance.",
            AlibiText = "Says he went home early, around 9 PM.",
            IsKiller = false
        };

        var suspects = new List<Suspect> { priya, dev, sarah, tom, alex };

        var newCase = new Case
        {
            Id = caseId,
            Title = "Segfault at Midnight",
            IntroText = "Co-founder Marcus Chen was found dead in the server room, hours before Nexlify's Series B demo. The police don't read code. You do.",
            VictimName = "Marcus Chen, CTO",
            SolutionText = "Priya Patel killed Marcus Chen to stop him from disclosing a security flaw that would have collapsed the funding round.",
            KillerSuspectId = null // set after suspects exist, to avoid a circular FK dependency
        };

        context.Cases.Add(newCase);
        context.SaveChanges(); // save the case first, with no killer set yet

        context.Suspects.AddRange(suspects);
        context.SaveChanges(); // now the suspects exist, satisfying Suspect.CaseId

        newCase.KillerSuspectId = priya.Id;
        context.SaveChanges(); // now safe to point the case at its killer
    }
}
