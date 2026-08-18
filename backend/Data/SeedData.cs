using CaseFile.Api.Models;

namespace CaseFile.Api.Data;

public static class SeedData
{
    public static void Seed(CaseFileDbContext context)
    {
        Case caseEntity;
        List<Suspect> suspects;

        if (!context.Cases.Any())
        {
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

            var newSuspects = new List<Suspect> { priya, dev, sarah, tom, alex };

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

            context.Suspects.AddRange(newSuspects);
            context.SaveChanges(); // now the suspects exist, satisfying Suspect.CaseId

            newCase.KillerSuspectId = priya.Id;
            context.SaveChanges();

            caseEntity = newCase;
            suspects = newSuspects;
        }
        else
        {
            caseEntity = context.Cases.First();
            suspects = [.. context.Suspects.Where(s => s.CaseId == caseEntity.Id)];
        }

        if (!context.Clues.Any())
        {
            SeedCluesAndEvidence(context, caseEntity, suspects);
        }
    }

    private static void SeedCluesAndEvidence(CaseFileDbContext context, Case caseEntity, List<Suspect> suspects)
    {
        var sarah = suspects.First(s => s.Name == "Sarah Kim");
        var priya = suspects.First(s => s.Name == "Priya Patel");

        var evidence1 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Corrected message timestamp",
            DescriptionText = "The bot log's real last-message time is 12:47 AM — the exact moment of death. Sarah Kim claimed she was alone in her office the whole time, but this message was sent from the shared team channel, not a private one.",
            LinkedSuspectId = sarah.Id
        };

        var evidence2 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Server room badge-in, 12:41 AM",
            DescriptionText = "Keycard logs show Priya Patel badged into the server room floor at 12:41 AM — six minutes before time of death. She never mentioned being there.",
            LinkedSuspectId = priya.Id
        };

        context.Evidence.AddRange(evidence1, evidence2);
        context.SaveChanges();

        var clue1 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 1,
            SourceLabel = "Slack Bot Logs",
            PuzzleType = "fix_bug",
            PromptText = "The bot log timestamps look off. Fix `getLastMessageTime` so it returns the last message's actual timestamp instead of `undefined`.",
            StarterCode = "function getLastMessageTime(messages) {\n  // BUG: off-by-one causes an out-of-bounds access\n  let lastIndex = messages.length;\n  return messages[lastIndex];\n}",
            Language = "javascript",
            EvidenceId = evidence1.Id,
            FunctionName = "getLastMessageTime"
        };

        var clue2 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 2,
            SourceLabel = "Keycard Access DB",
            PuzzleType = "write_function",
            PromptText = "Write `getServerRoomAccess(logs)` that returns the names of everyone who badged into floor 4 (the server room floor) that night.",
            StarterCode = "function getServerRoomAccess(logs) {\n  // logs: array of { name, floor, time }\n  // TODO: return an array of names for entries where floor === 4\n}",
            Language = "javascript",
            EvidenceId = evidence2.Id,
            FunctionName = "getServerRoomAccess"
        };

        context.Clues.AddRange(clue1, clue2);
        context.SaveChanges();

        // --- Test cases for Clue 1 ---

        var clue1Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue1.Id,
                Input = "[1200, 1215, 1247]",
                ExpectedOutput = "1247",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue1.Id,
                Input = "[600]",
                ExpectedOutput = "600",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue1.Id,
                Input = "[100, 200, 300, 400, 500]",
                ExpectedOutput = "500",
                IsHidden = true
            }
        };

        // --- Test cases for Clue 2 ---

        var clue2Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue2.Id,
                Input = "[{\"name\":\"Priya Patel\",\"floor\":4,\"time\":\"12:41\"},{\"name\":\"Dev Okafor\",\"floor\":2,\"time\":\"11:50\"}]",
                ExpectedOutput = "[\"Priya Patel\"]",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue2.Id,
                Input = "[{\"name\":\"Tom Reyes\",\"floor\":4,\"time\":\"11:00\"},{\"name\":\"Alex Park\",\"floor\":4,\"time\":\"9:05\"},{\"name\":\"Sarah Kim\",\"floor\":3,\"time\":\"10:15\"}]",
                ExpectedOutput = "[\"Tom Reyes\",\"Alex Park\"]",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue2.Id,
                Input = "[{\"name\":\"Dev Okafor\",\"floor\":2,\"time\":\"11:50\"}]",
                ExpectedOutput = "[]",
                IsHidden = true
            }
        };

        context.PuzzleTestCases.AddRange(clue1Tests);
        context.PuzzleTestCases.AddRange(clue2Tests);
        context.SaveChanges();
    }
}
