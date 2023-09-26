namespace AnalyticsService.Transport.Controller

open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging
open Dapr
open FluentValidation
open AnalyticsService.Transport.Contracts

[<ApiController>]
[<Route("/batch-analytics")>]
type BatchAnalyticsController (logger : ILogger<BatchAnalyticsController>, statValidator: IValidator<BatchStatRequest>) =
    inherit ControllerBase()

    [<HttpPost("")>]
    [<Topic("mrf-pub-sub", "batch-finish")>]
    member _.ReceiveBatchStat([<FromBody>] request: BatchStatRequest) =
        let result =
            statValidator.ValidateAsync(request)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        if not(result.IsValid) then
            Results.BadRequest(result.Errors)
        else
            logger.LogInformation($"Received a document batch statistic.\n\t- Workflow ID: {request.WorkflowId}")
            Results.Ok()