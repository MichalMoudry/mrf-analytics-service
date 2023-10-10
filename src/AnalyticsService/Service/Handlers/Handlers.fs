namespace AnalyticsService.Service.Handlers

open System
open System.Data
open System.Threading.Tasks
open MediatR
open AnalyticsService.Database.Domain
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests

/// A handler for retrieving generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQueryHandler() =
    interface IRequestHandler<GetGenericStatsQuery, string> with
        member _.Handle(_, cancellationToken) =
            if cancellationToken.IsCancellationRequested then ()

            Task.FromResult("{
                AvgProcessingTime = TimeSpan.FromMinutes(1)
                SuccessRate = 97.4
                AvgNumberOfDocs = 5.0
            }")

/// A handler for inserting a new batch stat to the database.
[<Sealed>]
type InsertBatchStatCommandHandler(conn: IDbConnection) =
    interface IRequestHandler<InsertBatchStatCommand, bool> with
        member this.Handle(request, cancellationToken) =
            if cancellationToken.IsCancellationRequested then ()
            let stat = NewBatchStat DateTime.Now DateTime.Now 5 BatchStatus.Success
            BatchStatRepository.insertRecord stat.Value conn
            |> Async.AwaitTask
            |> Async.RunSynchronously
            |> ignore
            Task.FromResult(false)
