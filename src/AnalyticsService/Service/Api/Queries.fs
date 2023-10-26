namespace AnalyticsService.Service.Api.Requests

open System
open AnalyticsService.Service.Dto
open MediatR

/// A query for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQuery(workflowId: Guid) =
    interface IRequest<GeneralAppStats>
    member _.WorkflowId = workflowId

/// A query for obtaining generic stats for a specific time period.
[<Sealed>]
type GetStatsForPeriodQuery(workflowId: Guid, startDate: DateTime, period: TimeSpan) =
    interface IRequest<GeneralAppStats>
    member _.WorkflowId = workflowId
    member _.StartDate = startDate
    member _.Period = period