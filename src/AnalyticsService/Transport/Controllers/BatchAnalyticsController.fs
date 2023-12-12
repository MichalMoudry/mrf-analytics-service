namespace AnalyticsService.Transport.Controllers

open Microsoft.AspNetCore.Mvc
open MediatR
open System
open Microsoft.AspNetCore.Http
open AnalyticsService.Service.Queries
open AnalyticsService.Transport.Contracts.Requests

/// A controller class for handling requests related to statistics.
[<Sealed>]
[<ApiController>]
[<Route("batch-analytics")>]
type BatchAnalyticsController(mediator: IMediator) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.GetStatsForWorkflow([<FromBody>] workflowId: Guid) =
        task {
            let! stats = mediator.Send(
                GetGenericStatsQuery(workflowId)
            )
            return Results.Ok(stats)
        }

    [<HttpGet("period")>]
    member _.GetPeriodStatsForWorkflow(request: GeneralBatchStatsForPeriod) =
        task {
            let! stats = mediator.Send(
                GetStatsForPeriodQuery(
                    request.WorkflowId,
                    request.StartDate,
                    request.EndDate - request.StartDate
                )
            )
            return Results.Ok(stats)
        }
