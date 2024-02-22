module AnalyticsService.Util.Extensions

open AnalyticsService.Transport.Contracts.Requests
open AnalyticsService.Transport.Validation
open FluentValidation
open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    member services.AddValidators() =
        services
            .AddScoped<IValidator<BatchStatsRequest>, BatchStatsRequestValidator>()
            .AddScoped<IValidator<BatchPeriodStatsRequest>, BatchPeriodStatsRequestValidator>()