namespace AnalyticsService.Service.Api.Requests

open MediatR

/// Command for inserting a new stat to the database.
[<Sealed>]
type InsertBatchStatCommand(data) =
    interface IRequest<bool>
    member _.Data = data
