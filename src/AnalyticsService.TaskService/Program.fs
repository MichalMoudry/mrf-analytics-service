namespace AnalyticsService.TaskService

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting

[<Sealed>]
module Program =
    [<EntryPoint>]
    let main args =
        let builder = Host.CreateApplicationBuilder(args)
        builder.Services.AddHostedService<ArchiveService>() |> ignore

        builder.Build().Run()

        0 // exit code