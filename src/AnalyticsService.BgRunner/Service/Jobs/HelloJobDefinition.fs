[<Sealed>]
module AnalyticsService.BgRunner.Service.Jobs.HelloJobDefinition

open System
open Quartz

type HelloJob() =
    interface IJob with
        member this.Execute _ =
            task {
                printfn $"{DateTime.Now}: Hello from a test job!"
                return ()
            }

let trigger: Action<ITriggerConfigurator> =
    Action<ITriggerConfigurator>(
        fun trigger ->
            trigger
                .WithIdentity("Hello job trigger")
                .WithSimpleSchedule(fun s ->
                    s.WithInterval(TimeSpan.FromSeconds(3)).RepeatForever() |> ignore
                )
                .ForJob(nameof(HelloJob)) |> ignore
    )
