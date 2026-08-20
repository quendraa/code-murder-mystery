using CaseFile.Api.Services;

namespace CaseFile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<IClueService, ClueService>();
        services.AddScoped<IGradingService, GradingService>();
        services.AddScoped<IPlayerProgressService, PlayerProgressService>();
        services.AddScoped<IEvidenceService, EvidenceService>();
        services.AddScoped<IPlayerService, PlayerService>();

        services.AddHttpClient<IJudgeService, JudgeService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Judge0BaseUrl"]!);
        });
        return services;
    }

}
