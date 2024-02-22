namespace AnalyticsService.Transport.Controllers

open AnalyticsService.Transport.Contracts.Requests
open FluentValidation
open MediatR
open Microsoft.AspNetCore.Mvc

[<ApiController; Sealed>]
[<Route("dapr")>]
type internal DaprController(
    mediator: IMediator,
    statsValidator: IValidator<BatchStatsRequest>
) =
    inherit ControllerBase()