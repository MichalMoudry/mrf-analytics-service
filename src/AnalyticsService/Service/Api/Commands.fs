namespace AnalyticsService.Service.Api.Requests

open System
open AnalyticsService.Database.Domain
open MediatR

/// Command for inserting a new stat to the database.
[<Sealed>]
type InsertBatchStatCommand(startDate: DateTime, endDate: DateTime, docsNumber: int, status: BatchStatus) =
    interface IRequest<bool>
    member _.StartDate = startDate
    member _.EndDate = endDate
    member _.NumberOfDocuments = docsNumber
    member _.Status = status
