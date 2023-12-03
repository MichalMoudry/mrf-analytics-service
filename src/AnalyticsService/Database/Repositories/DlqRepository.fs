[<Sealed>]
module AnalyticsService.Database.Repositories.DlqRepository

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL
open System

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

/// Method for selecting all items in a dead letter queue.
let GetDlqItems<'T> (conn: IDbConnection) =
    select {
        for _ in dlqTable do
            selectAll
    } |> conn.SelectAsync<'T>

/// Method for deleting a batch of DLQ items.
let DeleteDlqItems (conn: IDbConnection) (ids: list<Guid>) =
    delete {
        for item in dlqTable do
            where (ids |> List.contains item.Id)
    } |> conn.DeleteAsync
