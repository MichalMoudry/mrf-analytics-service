namespace AnalyticsService.Transport.Controller

open Microsoft.AspNetCore.Mvc

[<ApiController>]
[<Route("test")>]
type TestController() =
    inherit ControllerBase()

    [<HttpGet>]
    member _.Index() =
        "()"

    [<HttpPost>]
    member _.Post() =
        "()"
