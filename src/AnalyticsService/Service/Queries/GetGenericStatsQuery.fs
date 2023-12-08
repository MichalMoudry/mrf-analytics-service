namespace AnalyticsService.Service.Queries

open MediatR
open System

/// A query for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQuery(workflowId: Guid) =
    interface IRequest
    member _.WorkflowId = workflowId