/// Module containing code related to operations for a dead letter database table.
[<Sealed>]
module AnalyticsService.Database.Repositories.DlqRepository

open AnalyticsService.Database.Model
open Dapper.FSharp.PostgreSQL
open System
open System.Data

let private dlqTable = table'<DeadTopic> "DLQ"

let newDlqItem deadTopic (conn: IDbConnection) =
    task {
        conn.Open()
        use transaction = conn.BeginTransaction()
        
        transaction.Commit()
    }

/// Method for selecting all items from dead letter queue.
let getDlqItems<'T> (conn: IDbConnection) =
    select {
        for _ in dlqTable do
            selectAll
    } |> conn.SelectAsync<'T>
