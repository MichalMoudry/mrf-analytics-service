module AnalyticsService.BgRunner.Service.Extensions

open System
open System.Data
open AnalyticsService.BgRunner.Service.Jobs
open AnalyticsService.Database.Context
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Quartz

type IServiceCollection with
    member services.RegisterAdditionalServices(isProd: bool, cfg: IConfiguration) =
        let connStr =
            match isProd with
            | true -> Environment.GetEnvironmentVariable("DB_CONN")
            | false -> cfg["DbConnection"]
        services.AddTransient<IDbConnection>(fun i -> DbInit connStr) |> ignore
        services.AddQuartz(fun q -> (
            (*
            q.UsePersistentStore(fun q -> (
                q.UseProperties <- true
                q.UsePostgres(connStr)
                q.UseNewtonsoftJsonSerializer()
            ))
            *)
            q.ScheduleJob<HelloJobDefinition.HelloJob>(HelloJobDefinition.trigger)
                .ScheduleJob<ArchiveJobDefinition.ArchiveJob>(ArchiveJobDefinition.trigger) |> ignore
        ))
