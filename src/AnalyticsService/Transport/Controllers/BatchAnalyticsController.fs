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
[<Route("/batch-analytics")>]
type BatchAnalyticsController (
    logger : ILogger<BatchAnalyticsController>,
    mediator: IMediator,
    statValidator: IValidator<BatchStatRequest>) =
    inherit ControllerBase()

    [<HttpPost("")>]
    [<Topic("mrf-pub-sub", "batch-finish")>]
    member _.ReceiveBatchStat([<FromBody>] request: CloudEvent<BatchStatRequest>) =
        let result =
            statValidator.ValidateAsync(request.Data)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        if not(result.IsValid) then
            Results.BadRequest(result.Errors)
        else
            logger.LogInformation(
                $"Received a document batch statistic.\n\t- Workflow ID: {request.Data.WorkflowId}"
            )
            Results.Ok()

    [<HttpGet("")>]
    member this.GetStatsForAllBatches() =
        let stats =
            mediator.Send(GetGenericStatsForBatches())
            |> Async.AwaitTask
            |> Async.RunSynchronously
        Ok(stats)