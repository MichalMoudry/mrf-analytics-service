namespace AnalyticsService.Service.Api.Requests

open System
open AnalyticsService.Service.Dto
open MediatR

/// A query for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQuery(appId: Guid) =
    interface IRequest<GeneralAppStats>
    member _.AppId = appId

/// A query for obtaining generic stats for a specific time period.
[<Sealed>]
type GetStatsForPeriodQuery(appId: Guid, startDate: DateTime, period: TimeSpan) =
    interface IRequest<GeneralAppStats>
    member _.AppId = appId
    member _.StartDate = startDate
    member _.Period = period