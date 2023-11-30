module AnalyticsService.BgRunner.Service.Extensions

open System
open System.Data
open AnalyticsService.BgRunner.Service.Jobs
open AnalyticsService.Database.Context
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Quartz

type IServiceCollection with
    /// Method for registering additional services like IDbConnection.
    member services.RegisterAdditionalServices(isProd: bool, cfg: IConfiguration) =
        let connStr =
            match isProd with
            | true -> Environment.GetEnvironmentVariable("DB_CONN")
            | false -> cfg["DbConnection"]
        services
            .AddTransient<IDbConnection>(fun i -> DbInit connStr)
            .AddQuartz(fun q -> (
                q.ScheduleJob<HelloJobDefinition.HelloJob>(HelloJobDefinition.trigger)
                    .ScheduleJob<DlqJobDefinition.DlqJob>(DlqJobDefinition.trigger) |> ignore
            ))
