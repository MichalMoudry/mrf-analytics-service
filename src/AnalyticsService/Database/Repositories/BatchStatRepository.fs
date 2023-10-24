namespace AnalyticsService.Database.Repositories

open System
open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

/// A module providing queries for BatchStats table.
[<Sealed>]
module BatchStatRepository =
    let private statTable = table'<BatchStat> "BatchStats" |> inSchema "analytics"

    /// Method for inserting a new record into BatchStats table.
    let InsertRecord newStat (conn: IDbConnection) =
        task {
            conn.Open()
            use transaction = conn.BeginTransaction()
            conn.InsertAsync(
                insert {
                    into statTable
                    value newStat
                },
                transaction
            )
            |> Async.AwaitTask |> Async.RunSynchronously |> ignore
            transaction.Commit()
        }

    /// Method for obtaining records/stats for a specific application.
    let GetRecords (conn: IDbConnection, appId: Guid) =
        select {
            for stat in statTable do
                where (stat.AppId = appId)
                selectAll
        } |> conn.SelectAsync<BatchStat>

    /// Method for obtaining records/stats of a specific application for a specific period.
    let GetRecordsForPeriod (
        conn: IDbConnection,
        appId: Guid,
        startDate: DateTime,
        period: TimeSpan) =
        let endDate = startDate.Add(period)
        select {
            for stat in statTable do
                where (stat.AppId = appId
                       && stat.Created >= startDate
                       && stat.Created <= endDate)
        } |> conn.SelectAsync<BatchStat>
