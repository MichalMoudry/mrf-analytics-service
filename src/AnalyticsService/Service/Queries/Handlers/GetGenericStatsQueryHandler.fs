namespace AnalyticsService.Service.Queries.Handlers

open System
open System.Data
open AnalyticsService.Service.Commands
open AnalyticsService.Service.Model
open AnalyticsService.Database.Repositories
open MediatR

/// A handler for retrieving generic stats about all document batches in a specified workflow.
[<Sealed>]
type internal GetGenericStatsQueryHandler(
    conn: IDbConnection,
    batchRepo: IBatchStatRepository
) =
    let calculateSuccessRate all successful = all / float(successful)

    interface IRequestHandler<GetGenericStatsQuery, GeneralBatchStats> with
        member this.Handle(request, cancellationToken) =
            task {
                if not(cancellationToken.IsCancellationRequested) then
                    let stats =
                        batchRepo.GetBatchStats request.WorkflowId conn
                        |> Async.AwaitTask
                        |> Async.RunSynchronously
                        |> Seq.toList

                    if stats.Length = 0 then
                        return GeneralBatchStats.Default
                    else
                        return {
                            AvgNumberOfDocs =
                                stats
                                |> List.averageBy(fun i -> float i.NumberOfDocuments)
                                |> Math.Round
                                |> int
                            SuccessRate =
                                stats
                                |> List.filter(fun i -> i.IsSuccess)
                                |> List.length
                                |> calculateSuccessRate stats.Length
                            AvgDuration =
                                stats
                                |> List.averageBy(fun i -> float i.RunTime.Ticks)
                                |> int64
                                |> TimeSpan.FromTicks
                        }
                else
                    return GeneralBatchStats.Default
            }