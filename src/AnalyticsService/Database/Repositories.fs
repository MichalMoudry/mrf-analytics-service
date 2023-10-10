namespace AnalyticsService.Database.Repositories

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

[<Sealed>]
module BatchStatRepository =
    let statTable = table'<BatchStat> "batch_stats"
    
    let insertRecord newStat (conn: IDbConnection) =
        insert {
            into statTable
            value newStat
        } |> conn.InsertAsync

    let getRecords (conn: IDbConnection) =
        select {
            for stat in statTable do selectAll
        } |> conn.SelectAsync<BatchStat>
