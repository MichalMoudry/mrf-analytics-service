using AnalyticsService.Service.Model;
using MediatR;

namespace AnalyticsService.Service.Queries;

/// <summary>
/// A query for obtaining generic stats for a specific time period.
/// </summary>
internal sealed record GetStatsForPeriodQuery(
    Guid WorkflowId,
    DateTimeOffset StartDate,
    TimeSpan Period
) : IRequest<GeneralBatchStats>;