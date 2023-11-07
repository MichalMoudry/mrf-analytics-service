namespace AnalyticsService.Transport.Controllers

open AnalyticsService.Service.Api.Requests
open AnalyticsService.Transport.Contracts
open Dapr
open FluentValidation
open MediatR
open Microsoft.AspNetCore.Http
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Logging

[<ApiController>]
[<Sealed>]
[<Route("dapr")>]
type DaprController(
    logger : ILogger<DaprController>,
    mediator: IMediator,
    statValidator: IValidator<BatchStatRequest>) =
    inherit ControllerBase()
    
    [<HttpPost>]
    [<Topic("mrf-pub-sub", "batch-finish-stat", false, DeadLetterTopic = "poisonMessages")>]
    member _.ReceiveBatchStat([<FromBody>] request: CloudEvent<BatchStatRequest>) =
        let validationResult =
            statValidator.ValidateAsync(request.Data)
            |> Async.AwaitTask
            |> Async.RunSynchronously

        if not(validationResult.IsValid) then
            Results.BadRequest(validationResult.Errors)
        else
            logger.LogInformation($"Received a document batch statistic. {request.Data}")
            let result =
                mediator.Send(InsertBatchStatCommand(
                    request.Data.StartDate,
                    request.Data.EndDate,
                    request.Data.NumberOfDocuments,
                    request.Data.Status,
                    request.Data.WorkflowId
                ))
                |> Async.AwaitTask
                |> Async.RunSynchronously
            match result with
            | true -> Results.Ok()
            | false -> Results.StatusCode(StatusCodes.Status500InternalServerError)

    [<HttpPost("/failedMessages")>]
    [<Topic("mrf-pub-sub", "poisonMessages")>]
    member this.PoisonedMessages() =
        mediator.Send(
            InsertDlqEntryCommand(this.Request.Body)
        )
        |> Async.AwaitTask
        |> Async.RunSynchronously
        |> ignore
        Results.Ok()
