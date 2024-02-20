[<Sealed>]
module AnalyticsService.Database.Api.Extensions

open AnalyticsService.Database.Repositories
open Microsoft.Extensions.DependencyInjection

type IServiceCollection with
    /// An extension method for registering/adding repositories as services.
    member services.AddRepositories () =
        services
            .AddSingleton<IDlqRepository, DlqRepository>()
            .AddSingleton<IBatchStatRepository, BatchStatRepository>()
