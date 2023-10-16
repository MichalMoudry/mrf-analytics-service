namespace AnalyticsService.Service.Handlers

open System
open System.Data
open System.Threading.Tasks
open AnalyticsService.Database.Domain
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Service.Dto
open MediatR

/// A handler for retrieving generic stats about all document batches.
[<Sealed>]
type GetGenericStatsQueryHandler(conn: IDbConnection) =
    /// Method for calculating a success rate of document batches.
    let calculateSuccessRate all successful =
        float(successful) / all

    interface IRequestHandler<GetGenericStatsQuery, GeneralAppStats> with
        member _.Handle(request, cancellationToken) =
            if cancellationToken.IsCancellationRequested then ()
            let data =
                BatchStatRepository.GetRecords(conn, request.AppId)
                |> Async.AwaitTask
                |> Async.RunSynchronously
                |> Seq.toList
            let avgRunTime = query {
                for stat in data do
                    averageBy (float stat.RunTime.Ticks)
            }
            
            Task.FromResult({
                AverageNumberOfDocs = data
                    |> List.averageBy (fun i -> float i.NumberOfDocuments)
                    |> Math.Round
                    |> int
                SuccessRate = data
                    |> List.where (fun i -> i.Status = BatchStatus.Success)
                    |> List.length
                    |> calculateSuccessRate data.Length
                AverageDuration = TimeSpan.FromTicks(int64(avgRunTime))
            })
