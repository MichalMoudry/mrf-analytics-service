namespace AnalyticsService.Database.Repositories

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

/// A module providing queries for BatchStats table.
[<Sealed>]
module BatchStatRepository =
    let statTable = table'<BatchStat> "BatchStats"
    
    let InsertRecord newStat (conn: IDbConnection) =
        insert {
            into statTable
            value newStat
        } |> conn.InsertAsync

    let GetRecords (conn: IDbConnection) =
        select {
            for stat in statTable do selectAll
        } |> conn.SelectAsync<BatchStat>
