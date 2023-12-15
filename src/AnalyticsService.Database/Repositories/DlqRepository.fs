namespace AnalyticsService.Database.Repositories

open AnalyticsService.Database.Api
open AnalyticsService.Database.Model
open Dapper.FSharp.PostgreSQL
open System.Data

/// Module containing code related to operations for a dead letter database table.
[<Sealed>]
type DlqRepository =
    interface IDlqRepository with
        /// <inheritdoc/>
        member this.GetDlqItems(conn: IDbConnection) = 
            select {
                for _ in this.dlqTable do
                selectAll
            } |> conn.SelectAsync<DlqItemProcessingInfo>
    member private _.dlqTable = table'<DeadTopic> "DLQ"

    member this.NewDlqItem deadtopc (conn: IDbConnection) =
        task {
            conn.Open()
            use transaction = conn.BeginTransaction()
            transaction.Commit()
        }
