namespace AnalyticsService.BgRunner

open AnalyticsService.BgRunner.Service.Jobs
open Quartz
open Microsoft.Extensions.Hosting

module Program =
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                fun _ services -> (
                    services.AddQuartz(fun q -> (
                        q.ScheduleJob<HelloJobDefinition.HelloJob>(HelloJobDefinition.trigger) |> ignore
                    )) |> ignore
                    services.AddQuartzHostedService(fun options -> (
                        options.WaitForJobsToComplete <- true
                    )) |> ignore
                )
            )

    [<EntryPoint>]
    let main args =
        createHostBuilder(args).Build().Run()

        0 // exit code