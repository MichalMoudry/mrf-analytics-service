module AnalyticsService.BgRunner.Service.Jobs.DlqJobDefinition

open System
open System.Data
open System.Net.Http
open AnalyticsService.Database.Repositories
open AnalyticsService.BgRunner.Service.Model
open Quartz

type internal DlqJob(conn: IDbConnection, httpClientFactory: IHttpClientFactory) =
    interface IJob with
        member this.Execute _ =
            task {
                let items =
                    DlqRepository.GetDlqItems<DlqItem> conn
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
                    |> Seq.toList

                use client = httpClientFactory.CreateClient("localhost")
                items
                |> List.map (fun i -> i.RequestData)
                |> List.iter (fun i -> this.replayHttpRequest i client)

                (*
                    DlqRepository.DeleteDlqItems
                        conn
                        (items |> List.map (fun i -> i.Id))
                    |> ignore
                *)
                return ()
            }
    
    member this.replayHttpRequest (content: byte array) (client: HttpClient) =
        printfn $"Length: {content.Length}"
        ()

let trigger =
    Action<ITriggerConfigurator>(
        fun cfg ->(
            cfg.WithIdentity("Archive Job Trigger")
                //.WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(3, 0))
                .WithSimpleSchedule(fun s -> s.WithIntervalInSeconds(5).RepeatForever() |> ignore)
                |> ignore
        )
    )
