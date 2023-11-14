namespace AnalyticsService.Service.Handlers

open System.Data
open System.IO
open System.Text
open System.Text.Json
open AnalyticsService.Database
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Service.Dto
open MediatR

/// A handler class for InsertDlqEntryCommand command.
[<Sealed>]
type InsertDlqEntryCommandHandler(conn: IDbConnection) =
    interface IRequestHandler<InsertDlqEntryCommand, bool> with
        member this.Handle(request, cancellationToken) =
            task {
                cancellationToken.ThrowIfCancellationRequested()
                use streamReader = new StreamReader(request.RequestBody)
                let content =
                    streamReader.ReadToEndAsync()
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
                let event = JsonSerializer.Deserialize<CloudEvent<obj>>(content)
                let newDeadTopic =
                    Domain.NewDeadTopic
                        "/dapr/ReceiveBatchStat"
                        (Encoding.UTF8.GetBytes content)
                        event.Source
                DlqRepository.NewDlqItem newDeadTopic conn
                    |> Async.AwaitTask
                    |> Async.RunSynchronously
                return true
            }
