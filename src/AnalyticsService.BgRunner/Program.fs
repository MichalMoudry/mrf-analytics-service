namespace AnalyticsService.BgRunner

open AnalyticsService.BgRunner.Service.Extensions
open Quartz
open Microsoft.Extensions.Hosting

module Program =
    let createHostBuilder args =
        Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                fun ctx services -> (
                    services
                        .RegisterAdditionalServices(
                            ctx.HostingEnvironment.IsProduction(),
                            ctx.Configuration
                        )
                        .AddQuartzHostedService(fun options -> (
                            options.WaitForJobsToComplete <- true
                        ))
                        //.RegisterHttpClients()
                        |> ignore
                    services.RegisterHttpClients() |> ignore
                )
            )

    [<EntryPoint>]
    let main args =
        createHostBuilder(args).Build().Run()

        0 // exit code