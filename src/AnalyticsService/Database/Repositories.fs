namespace AnalyticsService.Database.Repositories

open System
open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

/// A module providing queries for BatchStats table.
[<Sealed>]
module BatchStatRepository =
    let statTable = table'<BatchStat> "BatchStats" |> inSchema "analytics"
    
    let InsertRecord newStat (conn: IDbConnection) =
        insert {
            into statTable
            value newStat
        } |> conn.InsertAsync

    let GetRecords (conn: IDbConnection, appId: Guid) =
        select {
            for stat in statTable do
                where (stat.AppId = appId)
                selectAll
        } |> conn.SelectAsync<BatchStat>
