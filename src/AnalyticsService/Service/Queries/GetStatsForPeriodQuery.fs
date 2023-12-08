namespace AnalyticsService.Service.Queries

open MediatR
open System

/// A query for obtaining generic stats for a specific time period.
[<Sealed>]
type GetStatsForPeriodQuery(workflowId: Guid, startDate: DateTime, period: TimeSpan) =
    interface IRequest
    member _.WorkflowId = workflowId
    member _.StartDate = startDate
    member _.Period = period