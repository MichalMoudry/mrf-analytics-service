namespace AnalyticsService.Service.Queries

open AnalyticsService.Service.Model
open MediatR
open System

/// A query for obtaining generic stats for a specific time period.
[<Sealed>]
type GetStatsForPeriodQuery(workflowId: Guid, startDate: DateTimeOffset, period: TimeSpan) =
    interface IRequest<GeneralBatchStats>
    member _.WorkflowId = workflowId
    member _.StartDate = startDate
    member _.Period = period