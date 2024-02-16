using System.Text.Json;
using AnalyticsService.Service.Commands;
using AnalyticsService.Service.Queries;
using AnalyticsService.Transport.Contracts.Requests;
using AnalyticsService.Transport.Contracts.Responses;
using Dapr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.Transport;

internal static class Handler
{
    private const string PubSubName = "pub-sub";

    /// <summary>
    /// A method for mapping endpoints to a web application.
    /// </summary>
    public static void Initialize(WebApplication app)
    {
        app.MapPost(
            "/dapr/batch-statistic",
            [Topic(PubSubName, "batch-finish-stat", false, DeadLetterTopic = "statistic-poison")]
            async (IValidator<BatchStatRequest> validator, IMediator mediator, CloudEvent<BatchStatRequest> request) =>
            {
                var validationResult = await validator.ValidateAsync(request.Data);
                if (!validationResult.IsValid)
                {
                    return TypedResults.ValidationProblem(validationResult.ToDictionary());
                }
                var res = await mediator.Send(new InsertBatchStatCommand(
                    request.Data.StartDate,
                    request.Data.EndDate,
                    request.Data.NumberOfDocuments,
                    request.Data.Status,
                    request.Data.WorkflowId
                ));
                return (IResult)(res ? TypedResults.Ok() : TypedResults.Problem());
            }
        )
        .WithName("PostBatchStatistic")
        .WithDescription("Endpoint for receiving results of processed document batches.")
        .WithTags("Dapr")
        .WithOpenApi();

        app.MapPost(
            "/failed-messages",
            [Topic(PubSubName, "statistic-poison")]
            async ([FromBody] JsonElement incomingObject, IMediator mediator) =>
            {
                var res = await mediator.Send(new InsertDlqEntryCommand(incomingObject));
                return (IResult)(res ? TypedResults.Ok() : TypedResults.Problem());
            }
        )
        .WithName("PoisonedMessages")
        .WithDescription("Endpoint for receiving of messages that the service failed to process.")
        .WithTags("Dapr")
        .WithOpenApi();

        app.MapGet(
            "/batch-analytics/{workflowId:guid}",
            async (Guid workflowId, IMediator mediator)
                => TypedResults.Ok(await mediator.Send(new GetGenericStatsQuery(workflowId)))
        )
        .WithName("GetBatchStatistics")
        .WithTags("Statistics")
        .WithOpenApi();

        app.MapGet("/batch-analytics/period", async ([FromBody] BatchPeriodStatRequest request, IMediator mediator) =>
        {
            var stats = await mediator.Send(new GetStatsForPeriodQuery(
                request.WorkflowId,
                request.StartDate,
                request.EndDate - request.StartDate
            ));
            return TypedResults.Ok(new StatisticResponse(stats));
        })
        .WithName("GetBatchStatisticsForPeriod")
        .WithDescription("Endpoint for receiving statistics about document batch for a specified period.")
        .WithTags("Statistics")
        .WithOpenApi();
    }
}