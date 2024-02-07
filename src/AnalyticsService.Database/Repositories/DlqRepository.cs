using System.Data;
using AnalyticsService.Database.Api;
using AnalyticsService.Database.Model.Domain;
using AnalyticsService.Database.Model.Dto;
using AnalyticsService.Database.Queries;
using Dapper;

namespace AnalyticsService.Database.Repositories;

/// <inheritdoc/>
public sealed class DlqRepository : IDlqRepository
{
    /// <inheritdoc/>
    public async Task NewDlqTopic(IDbConnection conn, DeadTopic topic)
    {
        conn.Open();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(Query.InsertDlqTopic, topic);
        transaction.Commit();
    }

    /// <inheritdoc/>
    public Task<IEnumerable<DeadTopicInfo>> GetDlqItems(IDbConnection conn, int limit = 1_000)
    {
        conn.Open();
        return conn.QueryAsync<DeadTopicInfo>(Query.GetDlqItems);
    }

    /// <inheritdoc/>
    public async Task DeleteDlqItems(IDbConnection conn, IEnumerable<Guid> ids)
    {
        conn.Open();
        using var transaction = conn.BeginTransaction();
        await conn.ExecuteAsync(
            Query.DeleteDlqItems,
            ids.Select(i => new { Id = i }),
            transaction
        );
        transaction.Commit();
    }
}