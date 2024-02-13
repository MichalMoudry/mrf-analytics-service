using System.Data;
using AnalyticsService.Database.Api;
using AnalyticsService.Service.Model;
using MediatR;

namespace AnalyticsService.Service.Queries.Handlers;

/// <summary>
/// A handler for retrieving generic stats about all document batches in a specified workflow.
/// </summary>
internal sealed class GetGenericStatsQueryHandler(
    IDbConnection conn,
    IBatchStatRepository batchStatRepository)
    : IRequestHandler<GetGenericStatsQuery, GeneralBatchStat>
{
    public async Task<GeneralBatchStat> Handle(GetGenericStatsQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return new GeneralBatchStat(0, 0, TimeSpan.Zero);
        }
        var stats = (await batchStatRepository
            .GetBatchStats(conn, request.WorkflowId))
            .ToList();
        var successRate = CalculateSuccessRate(
            stats.Count,
            stats.Count(i => i.IsSuccess())
        );

        return new GeneralBatchStat(
            (int)stats.Average(s => s.NumberOfDocuments),
            successRate,
            TimeSpan.FromTicks((long)stats.Average(d => d.RunTime.Ticks))
        );
    }

    /// <summary>
    /// Method for calculating a success rate of document batches.
    /// </summary>
    private static float CalculateSuccessRate(int all, int successful)
        => all / (float)successful;
}