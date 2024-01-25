using System.Data;
using AnalyticsService.Database.Model.Dto;

namespace AnalyticsService.Database.Api;

public interface IDlqRepository
{
    /// <summary>
    /// Method for inserting a new row into the DLQ table.
    /// </summary>
    Task NewDlqTopic(IDbConnection conn);

    /// <summary>
    /// Method for selecting a limited amount of items in a dead letter queue.
    /// </summary>
    Task<IEnumerable<DeadTopicInfo>> GetDlqItems(IDbConnection conn, int limit = 1_000);

    /// <summary>
    /// /// Method for deleting a batch of DLQ items based on supplied identifiers.
    /// </summary>
    Task DeleteDlqItems(IDbConnection conn, IEnumerable<Guid> ids);
}