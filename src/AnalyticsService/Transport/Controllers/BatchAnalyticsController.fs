namespace AnalyticsService.Transport.Controller

open System
open MediatR
open Microsoft.AspNetCore.Authorization
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open FluentValidation
open AnalyticsService.Service.Api.Requests

/// A controller class for handling requests related to statistics.
[<ApiController>]
[<Authorize>]
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