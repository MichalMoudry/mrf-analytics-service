using AnalyticsService.Database.Model.Domain;

namespace AnalyticsService.Database.Model.Dto;

/// <summary>
/// A data-transfer-object representing a single result of document batch.
/// </summary>
public sealed class BatchStatInfo
{
    public Guid Id { get; init; }

    public DateTimeOffset StartDate { get; init; }

    public DateTimeOffset EndDate { get; init; }

    public int NumberOfDocuments { get; init; }

    public TimeSpan RunTime { get; init; }

    public BatchStatus Status { get; init; }

    /// <summary>
    /// Method for determining if statistic is connected to a successful document batch completetion.
    /// </summary>
    public bool IsSuccess() => Status == BatchStatus.Completed;
}