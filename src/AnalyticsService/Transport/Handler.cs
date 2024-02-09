using AnalyticsService.Service.Commands;
using AnalyticsService.Service.Queries;
using AnalyticsService.Transport.Contracts.Requests;
using Dapr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnalyticsService.Transport;

internal static class Handler
{
    /// <summary>
    /// A method for mapping endpoints to a web application.
    /// </summary>
    public static void Initialize(WebApplication app)
    {
        app.MapPost("/dapr/batch-statistic", async (IValidator<BatchStatRequest> validator, IMediator mediator, CloudEvent<BatchStatRequest> request) =>
        {
            var validationResult = await validator.ValidateAsync(request.Data);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var res = await mediator.Send(new InsertBatchStatCommand(
                request.Data.StartDate,
                request.Data.EndDate,
                request.Data.NumberOfDocuments,
                request.Data.Status,
                request.Data.WorkflowId
            ));
            return res ? TypedResults.Ok() : TypedResults.BadRequest();
        })
        .WithName("PostBatchStatistic")
        .WithDescription("Endpoint for receiving results of processed document batches.")
        .WithOpenApi();

        app.MapGet(
            "/batch-analytics/{workflowId:guid}",
            async (Guid workflowId, IMediator mediator)
                => TypedResults.Ok(await mediator.Send(new GetGenericStatsQuery(workflowId)))
        )
        .WithName("GetBatchStatistics")
        .WithOpenApi();

        app.MapGet("/batch-analytics/period", ([FromBody] BatchPeriodStatRequest request, IMediator mediator) =>
        {
            return TypedResults.Ok();
        })
        .WithName("GetBatchStatisticsForPeriod")
        .WithDescription("Endpoint for receiving statistics about document batch for a specified period.")
        .WithOpenApi();
    }
}