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
            if cancellationToken.IsCancellationRequested then ()
            let batch =
                NewBatchStat
                    request.StartDate
                    request.EndDate
                    request.NumberOfDocuments
                    request.Status
                    request.AppId
            if batch.IsSome then
                BatchStatRepository.InsertRecord batch.Value conn
                Task.FromResult(true)
            else
                Task.FromResult(false)
