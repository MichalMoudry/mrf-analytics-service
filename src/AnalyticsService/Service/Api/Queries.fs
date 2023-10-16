namespace AnalyticsService.Service.Api.Requests

open System
open AnalyticsService.Service.Dto
open MediatR

/// A request for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQuery(appId: Guid) =
    interface IRequest<GeneralAppStats>
    member _.AppId = appId