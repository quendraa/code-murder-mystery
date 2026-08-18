using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class GradingService(CaseFileDbContext context, IJudgeService judgeService, IPlayerProgressService progressService) : IGradingService
{
    public async Task<SubmitSolutionResponse?> GradeSubmissionAsync(Guid clueId, string submittedCode, string sessionId)
    {
        var clue = await context.Clues
            .Include(c => c.TestCases)
            .Include(c => c.Evidence)
            .FirstOrDefaultAsync(c => c.Id == clueId);

        if (clue == null)
            return null;

        var results = new List<TestCaseResult>();

        foreach (var testCase in clue.TestCases)
        {
            var wrapperScript = $$"""
                {{submittedCode}}

                console.log(JSON.stringify({{clue.FunctionName}}({{testCase.Input}})));
                """;

            var (success, stdout, stderr) = await judgeService.RunCodeAsync(wrapperScript, "");

            var actualOutput = stdout?.Trim();
            var expectedOutput = testCase.ExpectedOutput.Trim();
            var passed = success && actualOutput == expectedOutput;

            results.Add(new TestCaseResult(
                Passed: passed,
                IsHidden: testCase.IsHidden,
                Input: testCase.IsHidden ? null : testCase.Input,
                ExpectedOutput: testCase.IsHidden ? null : testCase.ExpectedOutput,
                ActualOutput: testCase.IsHidden ? null : actualOutput,
                ErrorMessage: testCase.IsHidden ? null : stderr
            ));
        }

        var allPassed = results.All(r => r.Passed);

        if (allPassed)
        {
            await progressService.MarkClueSolvedAsync(clue.CaseId, sessionId, clue.Id);
        }

        return new SubmitSolutionResponse(
            AllTestsPassed: allPassed,
            Results: results,
            UnlockedEvidenceTitle: allPassed ? clue.Evidence?.Title : null
        );
    }
}
