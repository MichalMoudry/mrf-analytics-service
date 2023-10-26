namespace AnalyticsService.Transport.Controller

open System
open AnalyticsService.Transport.Contracts
open AnalyticsService.Service.Api.Requests
open MediatR
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open FluentValidation

/// A controller class for handling requests related to statistics.
[<ApiController>]
[<Authorize>]
[<Sealed>]
[<Route("batch-analytics")>]
type BatchAnalyticsController (mediator: IMediator) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetStatsForApp([<FromQuery>] appId: Guid) =
        let stats =
            mediator.Send(GetGenericStatsQuery(appId))
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Results.Ok(stats)

    [<HttpGet("period")>]
    member this.GetStatsForApp([<FromBody>] data: BatchPeriodStatsRequest) =
        let stats =
            mediator.Send(
                GetStatsForPeriodQuery(data.WorkflowId, data.StartDate, data.EndDate - data.StartDate)
            )
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Results.Ok(stats)