namespace AnalyticsService.Service.Commands.Handlers

open System.Data
open System.Text
open System.Text.Json
open AnalyticsService.Database.Model
open AnalyticsService.Database.Repositories
open AnalyticsService.Service.Commands
open AnalyticsService.Service.Model
open MediatR

/// A handler class for InsertDlqEntryCommand command.
[<Sealed>]
type internal InsertDlqEntryCommandHandler(
    conn: IDbConnection,
    dlqRepo: IDlqRepository
) =
    interface IRequestHandler<InsertDlqEntryCommand, bool> with
        member this.Handle(request, cancellationToken) =
            task {
                if not(cancellationToken.IsCancellationRequested) then
                    let cloudEvent =
                        JsonSerializer.Deserialize<CloudEvent<JsonElement>>(
                            request.Body
                        )
                    conn.Open()
                    use transaction = conn.BeginTransaction()
                    let ctx = { Conn = conn; Tx = transaction }

                    let deadLetter =
                        Constructors.NewDeadTopic
                            cloudEvent.Topic
                            (Encoding.UTF8.GetBytes(cloudEvent.Data.ToString()))
                            cloudEvent.Source
                    let! result = dlqRepo.NewDlqTopic deadLetter ctx
                    return result = 1
                else
                    return false
            }