module AnalyticsService.Extensions

open AnalyticsService.Transport.Contracts.Requests
open AnalyticsService.Transport.Validation
open FluentValidation
open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    /// Extension method for registering request validators.
    member services.RegisterValidators () =
        services
            .AddScoped<IValidator<BatchStatRequest>, BatchStatRequestValidator>()
