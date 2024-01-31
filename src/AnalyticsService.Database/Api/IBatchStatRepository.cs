using System.Data;
using AnalyticsService.Database.Model.Domain;

namespace AnalyticsService.Database.Api;

/// <summary>
/// A repository for document batch statistics.
/// </summary>
public interface IBatchStatRepository
{
    /// <summary>
    /// Method for inserting a new record into BatchStats table.
    /// </summary>
    Task AddNewBatchStat(IDbConnection conn, BatchStat stat);

    /// <summary>
    /// Method for obtaining records/stats for a specific workflow.
    /// </summary>
    Task GetBatchStats(IDbConnection conn, Guid workflowId);

    /// <summary>
    /// Method for obtaining records/stats of a specific application for a specific period.
    /// </summary>
    /// <param name="conn">A connection to a database.</param>
    /// <param name="startDate">Date from which stats should be considered.</param>
    /// <param name="period">Period for which stats should be considered.</param>
    Task GetBatchStats(IDbConnection conn, DateTimeOffset startDate, TimeSpan period);
}