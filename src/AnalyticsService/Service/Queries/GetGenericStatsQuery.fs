namespace AnalyticsService.Service.Queries

open AnalyticsService.Service.Model
open MediatR
open System

/// A query for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQuery(workflowId: Guid) =
    interface IRequest<GeneralBatchStats>
    member _.WorkflowId = workflowId