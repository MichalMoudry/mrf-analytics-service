namespace AnalyticsService.Service.Handlers

open System
open System.Threading.Tasks
open MediatR
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Service.Model.Dto

/// A handler for retrieving generic stats about all document batches.
[<Sealed>]
type GetGenericStatsForBatchesHandler() =
    interface IRequestHandler<GetGenericStatsForBatches, GeneralBatchStats> with
        member _.Handle(_, cancellationToken) =
            if cancellationToken.IsCancellationRequested then
                ()
            Task.FromResult({
                AvgProcessingTime = TimeSpan.FromMinutes(1)
                SuccessRate = 97.4
                AvgNumberOfDocs = 5.0
            })
