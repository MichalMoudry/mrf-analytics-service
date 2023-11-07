namespace AnalyticsService.Service.Handlers

open System.Data
open System.IO
open System.Text
open System.Text.Json
open System.Threading.Tasks
open AnalyticsService.Database.Repositories
open AnalyticsService.Database.Domain
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Service.Dto
open MediatR

/// A handler class for InsertDlqEntryCommand command.
[<Sealed>]
type InsertDlqEntryCommandHandler(conn: IDbConnection) =
    interface IRequestHandler<InsertDlqEntryCommand, bool> with
        member this.Handle(request, cancellationToken) =
            cancellationToken.ThrowIfCancellationRequested()
            use stream = new StreamReader(request.Data)
            let requestBody =
                stream.ReadToEndAsync()
                |> Async.AwaitTask
                |> Async.RunSynchronously
            let event = JsonSerializer.Deserialize<CloudEvent<obj>>(requestBody)
            DlqRepository.InsertRecord
                (NewDeadTopic (Encoding.UTF8.GetBytes($"{event.Data}")) event.Source)
                conn
                |> Async.AwaitTask
                |> Async.RunSynchronously

            Task.FromResult(true)
