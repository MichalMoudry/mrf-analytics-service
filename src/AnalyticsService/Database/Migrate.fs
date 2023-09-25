module AnalyticsService.Database.Migrate

open System
open Microsoft.Extensions.DependencyInjection
open FluentMigrator.Runner

let UpdateDatabase (serviceProvider: IServiceProvider) =
    serviceProvider
        .GetRequiredService<IMigrationRunner>()
        .MigrateUp()
