namespace AnalyticsService.Service.Handlers

open System
open System.Data
open System.Threading.Tasks
open AnalyticsService.Database.Domain
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Service.Dto
open MediatR

/// A handler class for obtaining general app stats for a period.
[<Sealed>]
type GetStatsForPeriodQueryHandler(conn: IDbConnection) =
    /// Method for calculating a success rate of document batches.
    let calculateSuccessRate all successful =
        float(successful) / all

    interface IRequestHandler<GetStatsForPeriodQuery, GeneralAppStats> with
        member this.Handle(request, cancellationToken) =
            if cancellationToken.IsCancellationRequested then
                Task.FromResult(GeneralAppStats.Default)
            else
                let data =
                    BatchStatRepository.GetRecordsForPeriod(
                        conn,
                        request.AppId,
                        request.StartDate,
                        request.Period
                    )
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
                    |> Seq.toList
                if data.Length = 0 then
                    Task.FromResult(GeneralAppStats.Default)
                else
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
