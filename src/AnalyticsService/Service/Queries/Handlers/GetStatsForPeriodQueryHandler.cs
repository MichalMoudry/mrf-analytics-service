using System.Data;
using AnalyticsService.Database.Api;
using AnalyticsService.Service.Model;
using MediatR;

namespace AnalyticsService.Service.Queries.Handlers;

/// <summary>
/// A handler class for obtaining general app stats for a period.
/// </summary>
internal sealed class GetStatsForPeriodQueryHandler : IRequestHandler<GetStatsForPeriodQuery, GeneralBatchStat>
{
    private readonly IDbConnection _dbConnection;

    private readonly IBatchStatRepository _batchStatRepository;

    public GetStatsForPeriodQueryHandler(IDbConnection conn, IBatchStatRepository repo)
    {
        _dbConnection = conn;
        _batchStatRepository = repo;
    }

    /// <inheritdoc/>
    public async Task<GeneralBatchStat> Handle(GetStatsForPeriodQuery request, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return GeneralBatchStat.Default;
        }
        var stats = (await _batchStatRepository
            .GetBatchStats(_dbConnection, request.StartDate, request.Period))
            .ToList();
        if (stats.Count == 0)
        {
            return GeneralBatchStat.Default;
        }

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