namespace AnalyticsService.Transport.Controller

open MediatR
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Dapr
open FluentValidation
open AnalyticsService.Service.Api.Requests
open AnalyticsService.Transport.Contracts

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
                mediator.Send(InsertBatchStatCommand(
                    request.Data.StartDate,
                    request.Data.EndDate,
                    request.Data.NumberOfDocuments,
                    request.Data.Status
                ))
                |> Async.AwaitTask
                |> Async.RunSynchronously
            match result with
            | true -> Results.Ok()
            | false -> Results.StatusCode(StatusCodes.Status500InternalServerError)


    [<HttpGet>]
    member _.GetStatsForUser() =
        let stats =
            mediator.Send(GetGenericStatsQuery())
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Results.Ok(stats)