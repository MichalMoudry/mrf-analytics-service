module AnalyticsService.BgRunner.Service.Jobs.ArchiveJobDefinition

open System
open System.Data
open AnalyticsService.Database.Repositories
open AnalyticsService.BgRunner.Service.Model
open Microsoft.FSharp.Control
open Quartz

type internal ArchiveJob(conn: IDbConnection) =
    interface IJob with
        member this.Execute ctx =
            task {
                let items =
                    DlqRepository.GetDlqItems<DlqItem> conn
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
                    |> Seq.toList
                return ()
            }

let trigger =
    Action<ITriggerConfigurator>(
        fun cfg ->(
            cfg.WithIdentity("Archive Job Trigger")
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(3, 0))
                .WithSimpleSchedule(fun s -> s.WithIntervalInSeconds(5).RepeatForever() |> ignore)
                |> ignore
        )
    )
