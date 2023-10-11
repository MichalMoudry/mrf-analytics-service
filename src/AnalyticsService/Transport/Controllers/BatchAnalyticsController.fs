namespace AnalyticsService.Transport.Controller

open MediatR
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Dapr
open FluentValidation
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Transport.Contracts
open AnalyticsService.Service.Api.Dto

[<ApiController>]
[<Route("batch-analytics")>]
type BatchAnalyticsController (
    logger : ILogger<BatchAnalyticsController>,
    mediator: IMediator,
    statValidator: IValidator<BatchStatRequest>) =
    inherit ControllerBase()

    [<HttpPost>]
    [<Topic("mrf-pub-sub", "batch-finish")>]
    member _.ReceiveBatchStat([<FromBody>] request: CloudEventV1<BatchStatRequest>) =
        let validationResult =
            statValidator.ValidateAsync(request.Data)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        if not(validationResult.IsValid) then
            Results.BadRequest(validationResult.Errors)
        else
            logger.LogInformation(
                $"Received a document batch statistic. {request.Data}"
            )
            let result =
                mediator.Send(InsertBatchStatCommand(request.Data))
                |> Async.AwaitTask
                |> Async.RunSynchronously
            match result with
            | true -> Results.Ok()
            | false -> Results.StatusCode(StatusCodes.Status500InternalServerError)


    [<HttpGet>]
    member _.GetStatsForAllBatches() =
        let stats =
            mediator.Send(GetGenericStatsQuery())
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Results.Ok(stats)