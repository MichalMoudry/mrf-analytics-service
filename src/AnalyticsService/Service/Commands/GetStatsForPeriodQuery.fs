namespace AnalyticsService.Service.Commands

open AnalyticsService.Service.Model
open MediatR
open System

/// A query for obtaining generic stats for a specific time period.
type GetStatsForPeriodQuery(
    workflowId: Guid,
    startDate: DateTimeOffset,
    period: TimeSpan
) =
    interface IRequest<GeneralBatchStats>
    member this.WorkflowId with get() = workflowId
    member this.StartDate with get() = startDate
    member this.Period with get() = period