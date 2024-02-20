namespace AnalyticsService.Database.Repositories

open AnalyticsService.Database.Model
open AnalyticsService.Database.Model.Domain
open AnalyticsService.Database.Model.Dto
open Dapper.FSharp.PostgreSQL

/// Repository for a dead letter queue.
[<Sealed>]
type DlqRepository() =
    let dlqTable = table'<DeadTopic> "DLQ"

    interface IDlqRepository with
        member this.DeleteDlqItems ids ctx =
            ctx.Conn.DeleteAsync(
                delete {
                    for item in dlqTable do
                        deleteAll
                },
                ctx.Tx
            )

        member this.GetDlqItems conn limit =
            select {
                for _ in dlqTable do
                    selectAll
                    take limit
            } |> conn.SelectAsync<DeadTopicInfo>

        member this.NewDlqTopic topic ctx =
            ctx.Conn.InsertAsync(
                insert {
                    into dlqTable
                    value topic
                },
                ctx.Tx
            )