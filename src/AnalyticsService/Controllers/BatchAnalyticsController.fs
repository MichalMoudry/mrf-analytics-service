namespace AnalyticsService.Controller
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

type BatchAnalyticsController (logger : ILogger<BatchAnalyticsController>) =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Get() =
        "test"