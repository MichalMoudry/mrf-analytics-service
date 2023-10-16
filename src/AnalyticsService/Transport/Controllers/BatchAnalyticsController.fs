namespace AnalyticsService.Transport.Controller

open System
open MediatR
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open FluentValidation
open AnalyticsService.Service.Api.Requests

[<ApiController>]
[<Authorize>]
[<Route("batch-analytics")>]
type BatchAnalyticsController (
    logger : ILogger<BatchAnalyticsController>,
    mediator: IMediator) =
    inherit ControllerBase()

    [<HttpGet>]
    member this.GetStatsForApp([<FromQuery>] appId: Guid) =
        let stats =
            mediator.Send(GetGenericStatsQuery(appId))
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Results.Ok(stats)