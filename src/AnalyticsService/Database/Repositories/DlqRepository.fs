[<Sealed>]
module AnalyticsService.Database.Repositories.DlqRepository

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

let private dlqTable = table'<DeadTopic> "DLQ" |> inSchema "analytics"

/// Method for inserting a new row into the DLQ table.
let NewDlqItem deadTopic (conn: IDbConnection) =
    task {
        conn.Open()
        use transaction = conn.BeginTransaction()
        conn.InsertAsync(
            insert {
                into dlqTable
                value deadTopic
            },
            transaction
        ) |> Async.AwaitTask |> Async.RunSynchronously |> ignore
        transaction.Commit()
    }
