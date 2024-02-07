using AnalyticsService.Database.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AnalyticsService.Database.Api;

public static class Extensions
{
    /// <summary>
    /// An extension method for registering/adding repositories as services.
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDlqRepository, DlqRepository>();
    }
}