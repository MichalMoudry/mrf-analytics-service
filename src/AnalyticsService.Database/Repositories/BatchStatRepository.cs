using System.Data;
using AnalyticsService.Database.Api;
using AnalyticsService.Database.Model.Domain;
using AnalyticsService.Database.Model.Dto;
using AnalyticsService.Database.Queries;
using Dapper;

namespace AnalyticsService.Database.Repositories;

/// <inheritdoc/>
public sealed class BatchStatRepository : IBatchStatRepository
{
    /// <inheritdoc/>
    public async Task AddNewBatchStat(IDbConnection conn, BatchStat stat)
    {
        conn.Open();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(Query.InsertBatchStat, stat);
        transaction.Commit();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<BatchStatInfo>> GetBatchStats(IDbConnection conn, Guid workflowId)
    {
        conn.Open();
        return conn.QueryAsync<BatchStatInfo>(Query.GetBatchStats, workflowId);
    }

    /// <inheritdoc/>
    public Task<IEnumerable<BatchStatInfo>> GetBatchStats(IDbConnection conn, DateTimeOffset startDate, TimeSpan period)
    {
        throw new NotImplementedException();
    }
}