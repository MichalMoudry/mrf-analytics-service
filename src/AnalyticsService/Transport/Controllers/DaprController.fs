namespace AnalyticsService.Transport.Controllers

open AnalyticsService.Transport.Contracts.Requests
open Dapr
open Microsoft.AspNetCore.Mvc
open Microsoft.AspNetCore.Http

[<Sealed>]
[<ApiController>]
[<Route("dapr")>]
type DaprController() =
    inherit ControllerBase()

    [<Topic("pub-sub", "batch-finish-stat", false, DeadLetterTopic = "analytics-poison-messages")>]
    [<HttpPost>]
    member _.ReceiveBatchStat ([<FromBody>] request: CloudEvent<BatchStatRequest>) =
        task {
            return Results.Ok()
        }