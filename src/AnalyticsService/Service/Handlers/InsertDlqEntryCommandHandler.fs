namespace AnalyticsService.Service.Handlers

open System.Data
open System.IO
open System.Text
open System.Threading.Tasks
open AnalyticsService.Database
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests
open MediatR

/// A handler class for InsertDlqEntryCommand command.
[<Sealed>]
type InsertDlqEntryCommandHandler(conn: IDbConnection) =
    interface IRequestHandler<InsertDlqEntryCommand, bool> with
        member this.Handle(request, cancellationToken) =
            use streamReader = new StreamReader(request.RequestBody)
            let content =
                streamReader.ReadToEndAsync()
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
            let newDeadTopic =
                Domain.NewDeadTopic
                    "Test"
                    (Encoding.UTF8.GetBytes content)
                    "test_service"
            DlqRepository.NewDlqItem newDeadTopic conn
                |> Async.AwaitTask
                |> Async.RunSynchronously
            Task.FromResult(true)
