module AnalyticsService.Extensions

open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    /// Extension method for registering request validators.
    member services.RegisterValidators () =
        services
