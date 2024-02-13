using AnalyticsService.Service.Model;
using MediatR;

namespace AnalyticsService.Service.Queries;

/// <summary>
/// A query for obtaining generic stats about all document batches.
/// </summary>
internal sealed record GetGenericStatsQuery(
    Guid WorkflowId
) : IRequest<GeneralBatchStat>;