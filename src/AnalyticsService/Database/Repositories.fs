namespace AnalyticsService.Database

open System.Data
open AnalyticsService.Database.Domain
open Dapper.FSharp.PostgreSQL

[<Sealed>]
type BatchStatRepository(conn: IDbConnection) =
    let statTable = table'<BatchStat> "BatchStats"
    
    let insertRecord newStat =
        insert {
            into statTable
            value newStat
        } |> conn.InsertAsync

    let getRecords =
        select {
            for stat in statTable do selectAll
        } |> conn.SelectAsync<BatchStat>
