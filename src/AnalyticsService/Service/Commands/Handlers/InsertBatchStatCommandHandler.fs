namespace AnalyticsService.Service.Commands.Handlers

open AnalyticsService.Service.Commands
open MediatR

/// A handler for inserting a new batch stat to the database.
[<Sealed>]
type internal InsertBatchStatCommandHandler() =
    interface IRequestHandler<InsertBatchStatCommand, bool> with
        member this.Handle(request, cancellationToken) =
            failwith "todo"