using CaseFile.Api.Data;
using CaseFile.Api.DTOs;
using CaseFile.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseFile.Api.Services;

public class PlayerService(CaseFileDbContext context) : IPlayerService
{
    public async Task<PlayerResponse> GetOrCreatePlayerAsync(string sessionId, string detectiveName)
    {

        var player = await context.Player
            .FirstOrDefaultAsync(p => p.SessionId == sessionId);

        if (player == null)
        {
            player = new Player
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                DetectiveName = detectiveName,
                CreatedAt = DateTime.UtcNow
            };

            context.Player.Add(player);
            await context.SaveChangesAsync();
        }

        return new PlayerResponse(player.Id, player.SessionId, player.DetectiveName, player.CreatedAt);
    }

    public async Task<PlayerResponse?> GetPlayerAsync(string sessionId)
    {
        var player = await context.Player.FirstOrDefaultAsync(p => p.SessionId == sessionId);

        return player == null
            ? null
            : new PlayerResponse(player.Id, player.SessionId, player.DetectiveName, player.CreatedAt);
    }
}
