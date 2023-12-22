namespace AnalyticsService.Database.Repositories

open System.Data
open AnalyticsService.Database.Api
open AnalyticsService.Database.Model
open Dapper.FSharp.PostgreSQL

/// A module providing queries for BatchStats table.
[<Sealed>]
module BatchStatRepository =
    let private statTable = table'<BatchStat> "BatchStat"

    /// Method for inserting a new record into BatchStats table.
    let insertStat newStat (conn: IDbConnection) =
        task {
            conn.Open()
            use transaction = conn.BeginTransaction()
            conn.InsertAsync(
                insert {
                    into statTable
                    value newStat
                }
            )
            |> Async.AwaitTask |> Async.RunSynchronously |> ignore
            transaction.Commit()
        }

    /// Method for obtaining records/stats for a specific workflow.
    let getStats workflowId (conn: IDbConnection) =
        select {
            for stat in statTable do
                where (stat.WorkflowId = workflowId)
                selectAll
        } |> conn.SelectAsync<BatchStatInfo>
