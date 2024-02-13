namespace AnalyticsService.Database.Model.Domain;

/// <summary>
/// Type representing a statistic about a single processed document batch.
/// </summary>
public sealed class BatchStat : Entity
{
    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public int NumberOfDocuments { get; init; }

    public TimeSpan RunTime => EndDate - StartDate;

    public BatchStatus Status { get; init; }

    public Guid WorkflowId { get; init; }

    public BatchStat()
    {
        Id = Guid.NewGuid();
        DateAdded = DateTime.UtcNow;
    }
}