using CaseFile.Api.Services;

namespace CaseFile.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICaseService, CaseService>();
        services.AddScoped<IClueService, ClueService>();
        return services;
    }

}
