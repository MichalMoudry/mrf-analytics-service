namespace AnalyticsService.Database.Model.Domain;

/// <summary>
/// Type representing a statistic about a single processed document batch.
/// </summary>
public sealed class BatchStat : Entity
{
    public DateTimeOffset StartDate { get; init; }

    public DateTimeOffset EndDate { get; init; }

    public int NumberOfDocuments { get; init; }

    public TimeSpan RunTime => EndDate - StartDate;

    public BatchStatus Status { get; init; }

    public Guid WorkflowId { get; init; }

    public BatchStat()
    {
        Id = Guid.NewGuid();
        DateAdded = DateTimeOffset.Now;
    }
}