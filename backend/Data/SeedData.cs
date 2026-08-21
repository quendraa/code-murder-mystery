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

        var evidence3 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Commit pushed 12:44 AM",
            DescriptionText = "Dev Okafor pushed a code change from dev.okafor@nexlify.io at 12:44 AM — three minutes before time of death. He claims he was never in the building.",
            LinkedSuspectId = suspects.First(s => s.Name == "Dev Okafor").Id
        };

        var evidence4 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Hidden camera footage, 12:40 AM",
            DescriptionText = "Buried in a corrupted folder structure, one clip is timestamped 12:40 AM — showing the server room hallway moments before the murder. The footage is grainy, but a shape moves past camera range."
        };

        var evidence5 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Decoded message: LET IT GO MARCUS",
            DescriptionText = "Buried in Marcus's unsaved code was a decoder for a message he'd received that night: \"LET IT GO MARCUS.\" Someone knew what he was planning to disclose — and wanted him to stop."
        };

        var evidence6 = new Evidence
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            Title = "Deleted message: Priya to Dev",
            DescriptionText = "A deleted message from Priya to Dev, still present in the raw chat export: \"push the auth patch now — don't worry about the failing healthcheck, marcus will want to see it in person before the demo anyway.\" She knew a failing healthcheck would pull Marcus into the server room himself.",
            LinkedSuspectId = suspects.First(s => s.Name == "Priya Patel").Id
        };

        context.Evidence.AddRange(evidence1, evidence2, evidence3, evidence4, evidence5, evidence6);
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

        var clue3 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 3,
            SourceLabel = "Git Commit History",
            PuzzleType = "regex",
            PromptText = "Extract the author's email and commit timestamp from a raw git log line. Write `extractCommitInfo(logLine)` returning `{ email, timestamp }`.",
            StarterCode = "function extractCommitInfo(logLine) {\n  // logLine looks like:\n  // \"commit a1b2c3 | dev.okafor@nexlify.io | 2026-01-14T00:44:00Z | Fix leak\"\n  // TODO: extract the email and timestamp\n}",
            Language = "javascript",
            FunctionName = "extractCommitInfo",
            EvidenceId = evidence3.Id
        };

        var clue4 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 4,
            SourceLabel = "Camera Metadata",
            PuzzleType = "recursion",
            PromptText = "The security footage folder is corrupted — files nested inside renamed subfolders, several layers deep. Write `findClip(node, targetTime)` that walks the folder tree and returns the file node whose timestamp matches the target, or null if none exists.",
            StarterCode = "function findClip(node, targetTime) {\n  // node: { type: \"folder\" | \"file\", name, children?, timestamp? }\n  // TODO: recursively search for the file with timestamp === targetTime\n}",
            Language = "javascript",
            FunctionName = "findClip",
            EvidenceId = evidence4.Id
        };

        var clue5 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 5,
            SourceLabel = "Marcus's Unsaved Code",
            PuzzleType = "fix_bug",
            PromptText = "Marcus's IDE has one unsaved file — a simple cipher decoder. Every letter in the encoded message was shifted forward by 3 (A→D, B→E, etc.). For example, \"HELLO\" encodes to \"KHOOR\". Fix decodeMessage(encoded) so it correctly shifts each letter back by 3 to reveal the original message. Spaces stay as spaces.",
            StarterCode = "function decodeMessage(encoded) {\n  return encoded\n    .split('')\n    .map(char => {\n      if (char === ' ') return ' ';\n      // BUG: shifting the wrong direction\n      const code = char.charCodeAt(0) + 3;\n      return String.fromCharCode(code > 90 ? code - 26 : code);\n    })\n    .join('');\n}",
            Language = "javascript",
            FunctionName = "decodeMessage",
            EvidenceId = evidence5.Id
        };

        var clue6 = new Clue
        {
            Id = Guid.NewGuid(),
            CaseId = caseEntity.Id,
            OrderIndex = 6,
            SourceLabel = "Chat Export (JSON)",
            PuzzleType = "parse_transform",
            PromptText = "The company chat export includes messages the UI shows as deleted — but they're still present in the raw data. Write `findDeletedMessages(chatExport)` that returns the text of every message where deleted === true.",
            StarterCode = "function findDeletedMessages(chatExport) {\n  // chatExport: array of { sender, text, deleted }\n  // TODO: return an array of text for messages where deleted === true\n}",
            Language = "javascript",
            FunctionName = "findDeletedMessages",
            EvidenceId = evidence6.Id
        };

        context.Clues.AddRange(clue1, clue2, clue3, clue4, clue5, clue6);
        context.SaveChanges();

        evidence3.ReinterpretedDescription = "Dev Okafor pushed a code change at 12:44 AM — but he didn't act alone. A deleted chat message shows Priya told him to push it, knowing the failing healthcheck it triggered would bring Marcus to the server room himself. Dev was used.";
        evidence3.ReinterpretedAfterClueId = clue6.Id;
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

        // --- Test cases for Clue 3 ---

        var clue3Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue3.Id,
                Input = "\"commit a1b2c3 | dev.okafor@nexlify.io | 2026-01-14T00:44:00Z | Fix leak\"",
                ExpectedOutput = "{\"email\":\"dev.okafor@nexlify.io\",\"timestamp\":\"2026-01-14T00:44:00Z\"}",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue3.Id,
                Input = "\"commit f9e8d7 | priya.patel@nexlify.io | 2026-01-14T00:41:00Z | hotfix\"",
                ExpectedOutput = "{\"email\":\"priya.patel@nexlify.io\",\"timestamp\":\"2026-01-14T00:41:00Z\"}",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue3.Id,
                Input = "\"commit 111aaa | sarah.kim@nexlify.io | 2026-01-13T23:58:00Z | notes\"",
                ExpectedOutput = "{\"email\":\"sarah.kim@nexlify.io\",\"timestamp\":\"2026-01-13T23:58:00Z\"}",
                IsHidden = true
            }
        };

        var clue4Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue4.Id,
                Input = "{ type: \"file\", name: \"clip1.mp4\", timestamp: \"12:40\" }, \"12:40\"",
                ExpectedOutput = "{\"type\":\"file\",\"name\":\"clip1.mp4\",\"timestamp\":\"12:40\"}",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue4.Id,
                Input = "{ type: \"folder\", name: \"root\", children: [{ type: \"folder\", name: \"a\", children: [{ type: \"folder\", name: \"b\", children: [{ type: \"file\", name: \"deep.mp4\", timestamp: \"12:40\" }] }] }] }, \"12:40\"",
                ExpectedOutput = "{\"type\":\"file\",\"name\":\"deep.mp4\",\"timestamp\":\"12:40\"}",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue4.Id,
                Input = "{ type: \"folder\", name: \"root\", children: [{ type: \"file\", name: \"clip2.mp4\", timestamp: \"09:00\" }] }, \"12:40\"",
                ExpectedOutput = "null",
                IsHidden = true
            }
        };

        var clue5Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue5.Id,
                Input = "\"KHOOR\"",
                ExpectedOutput = "\"HELLO\"",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue5.Id,
                Input = "\"WHVW\"",
                ExpectedOutput = "\"TEST\"",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue5.Id,
                Input = "\"OHW LW JR PDUFXV\"",
                ExpectedOutput = "\"LET IT GO MARCUS\"",
                IsHidden = true
            }
        };

        var clue6Tests = new List<PuzzleTestCase>
        {
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue6.Id,
                Input = "[{\"sender\":\"Priya\",\"text\":\"hey\",\"deleted\":false},{\"sender\":\"Priya\",\"text\":\"push that update now\",\"deleted\":true}]",
                ExpectedOutput = "[\"push that update now\"]",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue6.Id,
                Input = "[{\"sender\":\"Tom\",\"text\":\"dinner at 8?\",\"deleted\":false}]",
                ExpectedOutput = "[]",
                IsHidden = false
            },
            new()
            {
                Id = Guid.NewGuid(),
                ClueId = clue6.Id,
                Input = "[{\"sender\":\"Priya\",\"text\":\"push the auth patch now - don't worry about the failing healthcheck, marcus will want to see it in person before the demo anyway\",\"deleted\":true},{\"sender\":\"Dev\",\"text\":\"on it\",\"deleted\":false}]",
                ExpectedOutput = "[\"push the auth patch now - don't worry about the failing healthcheck, marcus will want to see it in person before the demo anyway\"]",
                IsHidden = true
            }
        };

        context.PuzzleTestCases.AddRange(clue1Tests);
        context.PuzzleTestCases.AddRange(clue2Tests);
        context.PuzzleTestCases.AddRange(clue3Tests);
        context.PuzzleTestCases.AddRange(clue4Tests);
        context.PuzzleTestCases.AddRange(clue5Tests);
        context.PuzzleTestCases.AddRange(clue6Tests);

        context.SaveChanges();
    }
}
