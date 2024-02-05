using AnalyticsService.Transport.Contracts.Requests;
using AnalyticsService.Transport.Validation;
using FluentValidation;

namespace AnalyticsService;

/// <summary>
/// A static class containing extension methods for <see cref="Program"/>.
/// </summary>
internal static class Extensions
{
    /// <summary>
    /// Extension method for registering request validators.
    /// </summary>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services
            .AddScoped<IValidator<BatchStatRequest>, BatchStatRequestValidator>()
            .AddScoped<IValidator<BatchPeriodStatRequest>, BatchPeriodStatRequestValidator>();
    }
}