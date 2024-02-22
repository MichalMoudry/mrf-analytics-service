namespace AnalyticsService.Service.Commands

open System
open MediatR

/// Command for inserting a new stat to the database.
[<Sealed>]
type internal InsertBatchStatCommand(
    startDate: DateTimeOffset,
    endDate: DateTimeOffset,
    docsNumber: int,
    status: int,
    workflowId: Guid
) =
    interface IRequest<bool>
    member this.StartDate = startDate
    member this.EndDate = endDate
    member this.DocsNumber = docsNumber
    member this.Status = status
    member this.WorkflowId = workflowId