namespace AnalyticsService.Service.Commands

open System.Text.Json
open MediatR

/// Command for inserting a new entry into the DLQ table.
[<Sealed>]
type internal InsertDlqEntryCommand(requestBody: JsonElement) =
    interface IRequest<bool>
    member this.Body = requestBody