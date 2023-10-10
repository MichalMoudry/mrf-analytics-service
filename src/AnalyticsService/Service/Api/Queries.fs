namespace AnalyticsService.Service.Api.Requests

open MediatR
open AnalyticsService.Service.Model.Dto

/// A request for obtaining generic stats about all document batches.
[<Sealed>]
type GetGenericStatsForBatches() =
    interface IRequest<GeneralBatchStats>