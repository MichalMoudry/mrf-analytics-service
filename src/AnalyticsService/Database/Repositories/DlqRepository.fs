namespace AnalyticsService.Database.Repositories

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

[<Sealed>]
module DlqRepository =
    let private statTable = table'<DeadTopic> "BatchStats" |> inSchema "analytics"

    let InsertRecord newMessage (conn: IDbConnection) =
        task {
            conn.Open()
            use transaction = conn.BeginTransaction()
            conn.InsertAsync(
                insert {
                    into statTable
                    value newMessage
                },
                transaction
            ) |> Async.AwaitTask |> Async.RunSynchronously |> ignore
            transaction.Commit()
        }
