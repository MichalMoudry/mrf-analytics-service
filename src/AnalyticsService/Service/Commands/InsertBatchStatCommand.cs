using AnalyticsService.Database.Model.Domain;
using MediatR;

namespace AnalyticsService.Service.Commands;

/// <summary>
/// Command for inserting a new stat to the database.
/// </summary>
internal sealed record InsertBatchStatCommand(
    DateTime StartDate,
    DateTime EndDate,
    int DocsNumber,
    BatchStatus Status,
    Guid WorkflowId
) : IRequest<bool>;