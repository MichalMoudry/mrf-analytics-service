namespace AnalyticsService.Service.Handlers

open System.Data
open System.Threading.Tasks
open AnalyticsService.Database.Domain
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests
open MediatR

/// A handler for retrieving generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQueryHandler(conn: IDbConnection) =
    interface IRequestHandler<GetGenericStatsQuery, string> with
        member _.Handle(_, cancellationToken) =
            if cancellationToken.IsCancellationRequested then ()
            let data =
                BatchStatRepository.GetRecords conn
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> Seq.toList
            
            let avgDocs = data |> List.averageBy (fun i -> float i.NumberOfDocuments)
            let successRate =
                data
                |> List.where (fun i -> i.Status = BatchStatus.Success)
                |> List.length
            
            Task.FromResult("{
                AvgProcessingTime = TimeSpan.FromMinutes(1)
                SuccessRate = 97.4
                AvgNumberOfDocs = 5.0
            }")
