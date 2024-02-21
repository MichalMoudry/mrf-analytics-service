namespace AnalyticsService.Database.Repositories

open AnalyticsService.Database.Model
open AnalyticsService.Database.Model.Domain
open AnalyticsService.Database.Model.Dto
open Dapper.FSharp.PostgreSQL
open System.Threading.Tasks

/// A repository for document batch statistics.
[<Sealed>]
type BatchStatRepository() =
    let statTable = table'<BatchStat> "BatchStat"

    interface IBatchStatRepository with
        member this.AddNewBatchStat stat ctx: Task =
            ctx.Conn.InsertAsync(
                insert {
                    into statTable
                    value stat
                },
                ctx.Tx
            )

        member this.GetBatchStats workflowId conn: Task<BatchStatInfo seq> =
            select {
                for stat in statTable do
                    where (stat.WorkflowId = workflowId)
                    selectAll
            } |> conn.SelectAsync<BatchStatInfo>

        member this.GetBatchStatsForPeriod workflowId startDate period conn: Task<BatchStatInfo seq> =
            let endDate = startDate.Add(period)
            select {
                for stat in statTable do
                    where (stat.WorkflowId = workflowId
                           && stat.DateAdded >= startDate
                           && stat.DateAdded <= endDate)
                    selectAll
            } |> conn.SelectAsync<BatchStatInfo>