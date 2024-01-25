namespace AnalyticsService.Database.Model.Domain;

/// <summary>
/// Type representing a statistic about a single processed document batch.
/// </summary>
public sealed class BatchStat : Entity
{
    public DateTimeOffset StartDate { get; init; }

    public DateTimeOffset EndDate { get; init; }

    public uint NumberOfDocuments { get; init; }

    public TimeSpan RunTime { get; init; }

    public short Status { get; init; }

    public Guid WorkflowId { get; init; }
}