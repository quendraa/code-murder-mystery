using CaseFile.Api.Services;

namespace CaseFile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<IClueService, ClueService>();

        services.AddHttpClient<IJudgeService, JudgeService>(client =>
        {
            client.BaseAddress = new Uri(configuration["Judge0BaseUrl"]!);
        });
        return services;
    }

}
