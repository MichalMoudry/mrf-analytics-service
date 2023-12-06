[<Sealed>]
module AnalyticsService.Repositories.DlqRepository

open AnalyticsService.Domain
open Dapper.FSharp.PostgreSQL
open System
open System.Data

let private dlqTable = table'<DeadTopic> "DLQ"

let internal NewDlqItem letter (conn: IDbConnection) =
    task {
        conn.Open()
        use transaction = conn.BeginTransaction()
        conn.InsertAsync(
            insert {
                into dlqTable
                value letter
            },
            transaction
        ) |> Async.AwaitTask |> Async.RunSynchronously |> ignore
        transaction.Commit()
    }

/// Method for selecting all items in a dead letter queue.
let internal GetDlqItems<'T> (conn: IDbConnection) =
    select {
        for _ in dlqTable do
            selectAll
    } |> conn.SelectAsync<'T>

/// Method for deleting a batch of DLQ items.
let internal DeleteDlqItems (conn: IDbConnection) (ids: list<Guid>) =
    delete {
        for item in dlqTable do
            where (ids |> List.contains item.Id)
    } |> conn.DeleteAsync
