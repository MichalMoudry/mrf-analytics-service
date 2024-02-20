/// A static class containing extension methods for Program class.
[<Sealed>]
module AnalyticsService.Web.Api.Extensions

open AnalyticsService.Web.Api.Transport.Contracts.Requests
open AnalyticsService.Web.Api.Transport.Validation
open FluentValidation
open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    /// Extension method for registering request validators.
    member services.AddValidators() =
        services
            .AddScoped<IValidator<BatchStatRequest>, BatchStatRequestValidator>()
            .AddScoped<IValidator<BatchPeriodStatRequest>, BatchPeriodStatRequestValidator>()