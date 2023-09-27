namespace AnalyticsService.Service.Handlers

open System.Threading.Tasks
open MediatR
open AnalyticsService.Service.Api.Requests

/// A handler for retrieving generic stats about all document batches.
type GetGenericStatsForBatchesHandler() =
    interface IRequestHandler<GetGenericStatsForBatches, string> with
        member _.Handle(_, cancellationToken) =
            if cancellationToken.IsCancellationRequested then
                ()
            Task.FromResult("Test value")