namespace AnalyticsService.Transport.Controllers

open AnalyticsService.Transport.Contracts
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc

/// A controller that is responsible for handling internal requests.
[<Sealed>]
[<ApiController>]
[<Route("internal")>]
type internal InternalController() =
    inherit ControllerBase()

    [<HttpPost("replay-stat")>]
    member _.ReplayStatRequest([<FromBody>] data: BatchPeriodStatsRequest) =
        Results.Ok()
