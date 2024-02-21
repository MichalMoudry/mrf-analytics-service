namespace AnalyticsService.Service.Commands

open AnalyticsService.Service.Model
open MediatR
open System

/// A query for obtaining generic stats about all document batches.
type internal GetGenericStatsQuery(workflowId: Guid) =
    interface IRequest<GeneralBatchStat>
    member this.WorkflowId with get() = workflowId