namespace AnalyticsService.Service.Handlers

open System.Data
open System.Threading.Tasks
open MediatR
open AnalyticsService.Database.Domain
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests

/// A handler for inserting a new batch stat to the database.
[<Sealed>]
type InsertBatchStatCommandHandler(conn: IDbConnection) =
    interface IRequestHandler<InsertBatchStatCommand, bool> with
        member this.Handle(request, cancellationToken) =
            (*
            if cancellationToken.IsCancellationRequested then
                Task.FromResult(false)
            else
                let batch =
                    NewBatchStat
                        request.StartDate
                        request.EndDate
                        request.NumberOfDocuments
                        request.Status
                        request.WorkflowId
                if batch.IsSome then
                    BatchStatRepository.InsertRecord batch.Value conn
                        |> Async.AwaitTask
                        |> Async.RunSynchronously
                    Task.FromResult(true)
                else
                    Task.FromResult(false)
            *)
            task {
                if cancellationToken.IsCancellationRequested then
                    return false
                else
                    let batch =
                        NewBatchStat
                            request.StartDate
                    return true
            }
