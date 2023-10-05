namespace AnalyticsService.Service.Api.Requests

open MediatR

/// A request for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsForBatches() =
    interface IRequest<string>