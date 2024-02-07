using AnalyticsService.Service.Commands;
using AnalyticsService.Transport.Contracts.Requests;
using Dapr;
using FluentValidation;
using MediatR;

namespace AnalyticsService.Transport;

internal static class Handler
{
    /// <summary>
    /// A method for mapping endpoints to a web application.
    /// </summary>
    public static void Initialize(WebApplication app)
    {
        app.MapPost("/dapr", async (IValidator<BatchStatRequest> validator, IMediator mediator, CloudEvent<BatchStatRequest> request) =>
        {
            var validationResult = await validator.ValidateAsync(request.Data);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            /*var res = await mediator.Send(new InsertBatchStatCommand(
                request.Data.StartDate,
                request.Data.EndDate,
                request.Data.NumberOfDocuments,
                request.Data.WorkflowId
            ));*/
            return true ? Results.Ok() : Results.BadRequest();
        });
    }
}